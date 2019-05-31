# 
Niche Name Sorter
=========
Developed by Michael Whitman

Sorts a given set of names, ordered by last name, then given names, performance tested across 4000 names.

### The Discrete Brief

The Goal: Name Sorter
Build a name sorter. Given a set of names, order that set first by last name, then by any given names the person may have. A
name must have at least 1 given name and may have up to 3 given names.


### Running the project

You will obviously need to create a text file in the .bin folder with a list of names to sort.

Ie. `unsorted-names-list.txt`, with some names such as:
```
Janet Parsons
Vaughn Lewis
Adonis Julius Archer
Shelby Nathan Yoder
Marin Alvarez
```

You can then run the project, passing in the name of the source file:

Ie. `NicheNameSorter ./unsorted-names-list.txt`

It will then produce the results but on screen and in a text file in the same directory: `./sorted-names-list.txt`.

**Any packages / bin / obj files are not committed to the repository, please ensure you build the project before running.**

### Project Notes

- It is not recommended to run this in a directory with `unsorted` in the name of it.
This is due to the way the algorithm infers the output file path.

- Have fun! :) 

**Special Notes**

- 100% Code coverage has not been yet achieved due to time restrictions .. but it's fairly close :-) .

- Yet to create a Travis CI Build pipeline.
