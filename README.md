# Bingo CLI

## Introduction
During my visit at my parents this Christmas I was introduced to the game of [Bingo on the German TV Channel NDR](https://www.ndr.de/fernsehen/sendungen/bingo_die_umweltlotterie/index.html). We all had a sheet, and as it seems to happen regularly, we all lost.

This made me curious: 
* What are actually the odds at winning at this game? 
* What if we change the size of the sheet? 
* What if we draw more numbers? 
* What if we change the range of numbers for each column? 

So I wrote a program ...

## The game of Bingo 
Before we get to the CLI, let's first understand the rules of the game. 

In Bingo, each participant has a _sheet_ with a 5x5 square table on it. In each table cell there are numbers. More particularily, the i-th column has random numbers between [1 + 15*i ... 15 + 15*i]. So the highest number possible is 5*15 = 75.

Now, the moderator now draws (in an excruciatingly slow process ) 22 numbers out of the 75 and announces them consecutively. If one of the numbers is found on the sheet of a participant, he marks it with an X, and if he completes a row, column, or one of the diagonals, he wins and has a Bingo.

## Bingo.CLI
Bingo is a command line tool to simulate the game of Bingo. You run it by simply typing 
```PS
.\Bingo.exe run
````
This will run a simulation of one pass of 100.000 games of Bingo with the standard rules described above. However, the fun does not stop there. You can pass in a multitude of flags to tweak the game, e.g., like this:
```PS
.\Bingo.exe run -n 250000 -p 3 -d 27 -s 6 -f 2 -g
````
In this example, the flag
* -n sets the number of games that shall be played (default 100.000)
* -p sets the number of passes the simulation will run through (default: 1),
* -d sets the number of numbers to be drawn (default: 22)
* -s sets the size of the Bongo sheet (default: 5)
* -f sets the sheet factor (default: 3)
* -g sets whether or not a random sheet shall be generated (statistically irrelevant, but nice)

All flags are independently useable (but may restrict each other, e.g., you can't draw more numbers that are available) and combinable.

For more information, just call
```PS
.\Bingo.exe run -h
````

## Feedback and Contributing
All feedback welcome!
All contributions are welcome!
