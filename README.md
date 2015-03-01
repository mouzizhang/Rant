<p align="center">
<img src="http://i.imgur.com/EZinvT5.png" alt="Rant logo"></img>
</p>

##What is Rant?

Rant is a language for adding rich variations to text. It combines a markup language with functional and imperative programming concepts to deliver a concise but powerful tool for procedurally generating text. The goal of Rant is to augment human creativity with the boundless potential of randomness, enabling content producers to consider their next idea as not just a concept, but a seed for countless possibilities.

###Features
Some of the highlights of what Rant offers:

* Recursive, branching choices with various selection strategies (random, ordered, locked...)
* Access to external dictionaries using queries
* Word rhyming using pronunciation metadata
* Synchronize sections of random output
* Automatic capitalization
* Indefinite article (a/an) automation
* Multiple outputs (channels)
* Probability modifiers
* Overwriting (targets)
* Looping (repeaters)
* Conditionals
* List objects
* Subroutines
* Arithmetic
* *And much, much more...*

##Examples

**Generate random sentence**
```
<name-male> likes to <verb-transitive> <noun.plural> with <pron.poss-male> pet <noun-animal> on <timenoun.plural-dayofweek>.
```
```
Alick likes to mount shuttlecocks with his pet bat on Mondays.
```

**Generate ten random numbers between 1 and 50 and spell them out using US spelling conventions**
```
[numfmt:verbal-en][rep:10][sep:,\s]{[num:1;50]}
```
```
thirteen, twenty two, thirteen, nineteen, thirty one, thirty four, forty two, twenty six, twelve, forty four
```

---

**Generate 32 random hexadecimal digits grouped into sets of eight**
```
[rep:4][sep:\s]{\8,x}

Alternatively...

[rep:32]{\x[notlast:[nth:8;0;\s]]}
```
```
6fb34d31 42e27a48 5884bce5 bf743ec8
```

---

**Insert text into existing output**
```
The following word is [get:N] characters long: \"[mark:a]<noun-animal|fruit>[mark:b]\"
[send:N;[dist:a;b]]
```
```
The following word is 7 characters long: "kumquat"
```

##NuGet
Rant is also available as a [NuGet package](https://www.nuget.org/packages/Rant/). Punch the following into your package manager console and smash the Enter key enthusiastically to get it:
```
PM> Install-Package Rant
```

##License
Rant is provided under [The MIT License](https://github.com/TheBerkin/Rant/blob/master/LICENSE).

##Improve Rant
If there is something you want fixed, added, or changed, feel free to submit an issue/pull request. You are welcome to help with any of the following:
* Documentation (wiki/code)
* Fixing bugs
* Optimization
* New functions
* New language features
* Improving old language features
* New API features
* Formatting support for other languages

We have a [Trello board](https://trello.com/b/NnvgqGha/rant) where we also track progress on new features and bugfixes.

##Learn Rant
See [rantlang.github.io](http://rantlang.github.io) for full documentation of the API and Rant language, as well as additional example code.

##Try Rant
Want to try Rant before you download anything? Check out [RantBox](http://rant.berkin.me/), an online pattern interpreter I made.

##Support Rant
Rant is free to use; however, developing it takes a lot of time and dedication, and things like hosting and software cost money. If you feel so inclined, you may support the project by donating through **[Gratipay](https://gratipay.com/TheBerkin/)**. Any amount is appreciated :)
