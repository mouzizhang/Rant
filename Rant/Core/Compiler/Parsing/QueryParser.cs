﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Rant.Core.Compiler.Syntax;
using Rant.Vocabulary.Querying;
using Rant.Core.Utilities;

namespace Rant.Core.Compiler.Parsing
{
	internal class QueryParser : Parser
	{
		public override IEnumerator<Parser> Parse(RantCompiler compiler, CompileContext context, TokenReader reader, Action<RantAction> actionCallback)
		{
			var tableName = reader.Read(R.Text, "query table name");
			var query = new Query();
			query.Name = tableName.Value;
			query.ClassFilter = new ClassFilter();
			query.RegexFilters = new List<_<bool, Regex>>();
			bool exclusiveRead = false;
			bool subtypeRead = false;

			while(!reader.End)
			{
				var token = reader.ReadLooseToken();

				switch(token.ID)
				{
					// read subtype
					case R.Subtype:
						// if there's already a subtype, throw an error and ignore it
						if(subtypeRead)
						{
							compiler.SyntaxError(token, "multiple subtypes in a query", false);
							reader.Read(R.Text, "query subtype name");
							break;
						}
						// if the exclusive sign has already been read, throw an error and ignore it
						if(exclusiveRead)
						{
							compiler.SyntaxError(token, "subtype should be before exclusive query sign.", false);
							reader.Read(R.Text, "query subtype name");
							break;
						}
						query.Subtype = reader.Read(R.Text, "query subtype").Value;
						subtypeRead = true;
						break;

					// read query
					case R.Hyphen:
						{
							bool blacklist = false;
							// check if it's a blacklist filter
							if(reader.PeekType() == R.Exclamation)
							{
								blacklist = true;
								reader.ReadToken();
							}
							var classFilterName = reader.Read(R.Text, "class filter rule");
							var rule = new ClassFilterRule(classFilterName.Value, !blacklist);
							query.ClassFilter.AddRule(rule);
						}
						break;

					// read regex filter
					case R.Without:
					case R.Question:
						{
							bool blacklist = (token.ID == R.Without);
							
							var regexFilter = reader.Read(R.Regex, "regex filter rule");
							var rule = new _<bool, Regex>(!blacklist, Util.ParseRegex(regexFilter.Value));
							query.RegexFilters.Add(rule);
						}
						break;

					// read exclusive sign
					case R.Dollar:
						exclusiveRead = true;
						query.Exclusive = true;
						break;

					// read syllable range
					case R.LeftParen:
						// There are four possible types of values in a syllable range:
						// (a), (a-), (-b), (a-b)

						// either (a), (a-), or (a-b)
						if(reader.PeekLooseToken().ID == R.Text)
						{
							var firstNumberToken = reader.ReadLooseToken();
							int firstNumber;
							if(!Util.ParseInt(firstNumberToken.Value, out firstNumber))
							{
								compiler.SyntaxError(firstNumberToken, "syllable range value is not a valid integer");
							}
							
							// (a-) or (a-b)
							if(reader.PeekLooseToken().ID == R.Hyphen)
							{
								reader.ReadLooseToken();
								// (a-b)
								if(reader.PeekLooseToken().ID == R.Text)
								{
									var secondNumberToken = reader.ReadLooseToken();
									int secondNumber;
									if(!Util.ParseInt(secondNumberToken.Value, out secondNumber))
									{
										compiler.SyntaxError(secondNumberToken, "syllable range value is not a valid integer");
									}

									query.SyllablePredicate = new Range(firstNumber, secondNumber);
								}
								// (a-)
								else
								{
									query.SyllablePredicate = new Range(firstNumber, null);
								}
							}
							// (a)
							else
							{
								query.SyllablePredicate = new Range(firstNumber, firstNumber);
							}
						}
						// (-b)
						else if(reader.PeekLooseToken().ID == R.Hyphen)
						{
							reader.ReadLooseToken();
							var secondNumberToken = reader.ReadLoose(R.Text, "syllable range value");
							int secondNumber;
							if(!Util.ParseInt(secondNumberToken.Value, out secondNumber))
							{
								compiler.SyntaxError(secondNumberToken, "syllable range value is not a valid integer");
							}
							query.SyllablePredicate = new Range(null, secondNumber);
						}
						// ()
						else if(reader.PeekLooseToken().ID == R.RightParen)
						{
							compiler.SyntaxError(token, "empty syllable range", false);
						}
						// (something else)
						else
						{
							compiler.SyntaxError(reader.PeekLooseToken(), "unexpected token in syllable range");
						}

						reader.ReadLoose(R.RightParen, "syllable range end");
						break;

					// end of query
					case R.RightAngle:
						goto end_of_loop;

					default:
						compiler.SyntaxError(token, "unexpected token");
						break;
				}
			}

			end_of_loop:

			actionCallback(new RAQuery(query, tableName));
			yield break;
		}
	}
}