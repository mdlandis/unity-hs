To run (on windows):

Run UnityHS.exe

Rules:

It would take too long to explain all of hearthstone's rules here, so here is a link to the actual game's rules:

http://www.ign.com/wikis/hearthstone-heroes-of-warcraft/Rules

Due to major features being cut, there are a few notable differences:
-There are only two classes: Warrior and Warlock.
-Decks are predetermined: The warlock uses a token zoo deck and the warrior uses a patron deck.
-The warlock always goes first.

And due to me not being an expert coder, other more subtle rules may not work correctly, for example:
- The order deathrattles resolve in
- Battlecrys activating before/after a minion is summoned

Depending on the situation, these may never impact the game.

Credit:

Parts of Code that came from other people's ideas:
- The fact that Minions/spells/weapons effects are built on Polymorphism came from some help from a chat room. He suggested I use an interface, which worked pretty well, I changed it to a superclass which worked even better.
- The List Shuffle method came from online.
- The file reading methods came from online, at least the outside parts did.
- The repeatedly used "ScreenToWorldPoint" setup with v3 was taken from online.
- the (Behavior)GetComponent("Halo") was taken from online.
- There is a debugging script that is hidden deep inside the program. It probably won't appear in the runnable part, but in case it does, it was from online.
- My only experience with C# and Unity is from the book "Unity in Action" which I read to get a basic understanding. Much of the architecture that I created at the beginning (having a CardController, and a SetCard method, and using Instantiate to create new cards) were adapted from the card game project in that book, since I have no other experience to go off of. Later on, I started experimenting and looking at solutions that better fit my situation, but I never changed the original code. 
- The card images were taken from another one of my projects which did a similar thing, which were downloaded from hearthpwn.com. If you mark me down for this, fine, but even with a low threshhold for quality I think it would have taken me far too long to create placeholder images for the 50+ cards I included. And I didn't want to have a project as complex and potentially useful as this one's progress restricted by artwork.
- The cards that I made up were generated with hearthcards.net, a widely used site for custom created hearthstone cards.
- Hopefully this is everything, I might have forgotten a few lines here or there. 

Documentation:


Only two or three of the classes are documented, I realized part of the way through that documenting the 50+ classes I had would take far too long.

The most important scripts are: CardController.cs, PlayerInput.cs, HandCard.cs, BoardCharacter.cs, and Player.acs.

Here is a brief outline of how the program works:

The CardController class reads in card information from text files and stores them in arrays as Card objects. It then creates two instances of the Player class and processes their decklists, creating HandCards (cards that can be played, located in the hand or deck) and placing them in the deck locations.

The game starts, Mulligan is done on turn 1, then the game is playable.

During a turn, a player can attack with each their various minions (or hero if they have a weapon) once per turn, and spend mana to play cards. Their maximum mana increments up to 10 and is refreshed every turn. 

The HandCard class processes its movement each frame. If a card is selected by the player, it moves with the mouse until dropped. Otherwise, it glides towards its location.

When a card is played, the HandCard is destroyed and the Minion that corresponds to it appears. If the minion has a targeted effect that activates when it comes into play (a battlecry) then the player is prompted to aim it. 

If the card was instead a spell or weapon, they activate their various effects.

The player's board (consisting of the minions they control) is stored with the player and automatically aligned. The maximum board size is 7.

Whenever a card is played, the script where it's effect is stored is loaded using the card's filename attribute. Each effect is a subclass of the Effect/SpellEffect/WeaponEffect superclass. A minion with an onPlay() effect overrides the superclass's onPlay(). A minion with a targeted battlecry overrides getBattlecryTarget() (to see if it needs to aim an effect or not), setBattlecryTarget() (to see if a valid target was chosen) and onPlay().

Minions can attack and have other effects that impact the board is some way.

Each player has a Hero Power which can be used once per turn.

There are other various polishes, like being able to magnify a card by mousing over it and having cards slide out of the way when one is being played, as well as damage labels and some very crude animations.

There are many, many unimplemented features. I plan to maybe try to add multiplayer. After that, if it happens, I'll retire the project, as seriously taking this anywhere would require it to be rebuilt from scratch.