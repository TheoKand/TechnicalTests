Battleship

I was planning to create separate classes for the model and separate classes for the Logic, UI, thus achieving some more separation of concerns. 
But this would not exceed the 2-3 hours that are suggested for the duration, so I opted for something simpler. Obviously it's not a good practice
to couple the high-level business rules with details such as the specific input/output implementation (console app), as it violates both the 
Single Responsibility principe and the Dependency Inversion pripciple.

Another improvement that I didn't go ahead with because of the time constraint is the introduction of some basic AI logic for the computer. I guess
it's ok for it to fire randomly but after a hit, it should try the neighboring squares in order to sink the ship.

The solution was created with Microsoft Visual Studio 2017