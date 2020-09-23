# LIMB GAME DESIGN
=====================
_1-4 players_

Main loop is dungeon diving -> using resources gathered to gain power in town -> dungeon diving

Game sequence:
1. Players must create a character:
- choose name/stats/skills/equipment
2. All players in the party delve into the dungeon. Once they enter the dungeon, they cannot easily escape
- each dungeon has multiple levels to complete
- Each Level is a random sequence of Events, which can be:
- battles
- challenges
- traps
- treasure
- exits
3. after the level's Delve is complete, the party will be given the chance to Exit and return to town or Continue to a deeper floor to Delve again with their current stats/levels
- if the party Exits, dungeon progress is reset. They will be able to use gold and XP to get new equipment and level up.

## Character Creation
1. Player chooses a race/char portrait
2. Player allocates stats
3. Player chooses a class based on allocated stats
4. Player chooses skills from a list of allowed starting skills for that class
5. Player buys equipment for the character using some starting gold budget

## Dungeons
Each dungeon has one or more Levels. All Levels must be completed to clear a dungeon.

EXAMPLES:

1. The beginner Forest area consists of 3 Levels.
2. The intermediate Mine area consists of 5 Levels.
3. The difficult Volcano area consists of 10 Levels. 

### Completing a Level
Each Level in a dungeon is described as a series of Phases. Each Phase consists of one or more Events. The Events could be fighting enemies, disarming/navigating through traps, or finding treasure.
Each Level requires a certain number of Phases to be completed. Once enough Phases have been completed in the Level, the adventurers can choose to continue to the next Level or exit back to Town.

EXAMPLES:
1. The beginner Forest Level 1 requires 5 Phases to be completed, with 1 Event each.
2. The intermediate Mine Level 1 requires 5 Phases to be completed, with the first 4 Phases having 1 Event and the last Phase having 2 Events.
3. The difficult Volcano Level 1 requires 5 Phases to be completed, with all Phases requiring 2 Events.

#### Splitting the party
Players do *not* have to stay together while in the dungeon. They can split themselves into however many parties they wish (even a party of 1). 
If the adventurers split the party, each separate party must complete separate Events during each Phase.
Players can also choose to split the party and to regroup in between Phases.
However, once a Phase starts, it must be completed/failed before splitting/regrouping again.
A party that completes the Events can weather dangers together, but will also have to split rewards. 
An adventurer that goes off by themselves can take all Event rewards for their own, but will risk danger alone.
Weigh the benefits of splitting the party carefully.

##### An example of splitting the party
A party of four adventurers (Alice, Bob, Charlotte, and Douglas) decide to delve into the Forest Dungeon.
On the first Level of the Dungeon, 5 Phases must be completed before moving on to the next Level.
After completing 2 Phases, the party decides to split into Alice & Bob, and Charlotte & Douglas. 
The third Phase of the Forest Level 1 requires 1 Event to be completed by each party.
Therefore, both the party of A&B and C&D have to complete an Event to complete the third Phase.

##### Coming to the rescue
If there are multiple parties that are in the middle of resolving an Event (AFTER they have revealed the Events, but BEFORE they have acted on them), one of the parties can choose to assist another party in their Event.
The assisted party will have the benefit of the assisting party's existence, and can proceed with their Event as if they were one party.
The assisting party will automatically fail their Event, and will take any penalties associated.
If a party's Event is a Battle, they cannot assist another party.

### Events
These are the different types of Events one can find in a Dungeon. 

#### Battles
Each Battle consists of a series of Fights. 
Each Battle has a difficulty (EASY, MEDIUM, or HARD).
Each Fight is between the party and 1 or more enemies. 
When all the enemies in a Fight have been defeated, the next Fight begins, along with a new set of enemies.

Example:

In a Battle between the adventurer party and a party of goblins, there are 3 Fights.

1. Fight 1 has two goblins
2. Fight 2 has two goblins
3. Fight 3 has one goblin

Each Fight must be completed to complete the Event.

##### Difficulty
The number of Fights in a Battle and the maximum number of enemies in each Fight is determined by the Battle's difficulty and the number of adventurers in the party.

Example:
(Assuming the enemies are all the same)
A solo adventurer's Easy Battle could have two Fights, with one enemy each.
A solo adventurer's Medium Battle could have two Fights, with the first having one enemy and the second having two.
A solo adventurer's Hard Battle could have three Fights, with all fights having two enemies.

#### Rewards
Upon completing a Battle, the party will be rewarded with gold, XP, and/or items. Sometimes, the enemies may even drop a Chest.

A Chest may contain more valuable resources, but often have devious traps that can harm the party.
Adventurers can attempt to inspect a Chest to determine what kind of trap is on it, if any.
Adventurers can also attempt to disarm the trap, allowing them to safely retrieve the Chests' contents.
If they fail, the trap might trigger, or the contents of the Chest may be destroyed.

#### Challenges and Traps
Sometimes the party will find obstacles in their way. It could be a locked door, a broken bridge, or a collapsed tunnel.
The party that comes across the Event must use their stats, skills, or resources to get past them.
If they are unable to use them successfully, they will be penalized.

Examples:
A locked door
	- An adventurer with lockpicking can try to pick the lock. If they fail, they must use some other method 
	- An adventurer can try to break the door down. Failing will result in them taking damage as the door breaks open. Those with higher strength are more likely to succeed.
	- A mage can attempt to blow the door open with a spell. It takes MP to do, and if it fails will harm another party member as it opens, or it will bring enemies to their location.
A wide gap
	- All adventurers in the party can attempt to leap across the gap. Those that fail are injured from the attempt, but make it past. 
		- Having Rope in someone's inventory will make this check easier.
	- A mage can cast a Levitation spell to get their party across, if they have the skill.

#### Treasure
Caches of valuable artifacts, equipment, and gold can be found in Dungeons. 

#### Exits

## Town
Can rest, buy items and equipment, and level up