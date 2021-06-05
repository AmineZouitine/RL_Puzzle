# 🧩 Built puzzles for my Agent  🧩 #

In this project, you will be able to create levels for my agent and see how he can solve them; you also can test the levels yourself to compare your score to the agents! 

https://user-images.githubusercontent.com/53370597/117895750-917eec00-b2ae-11eb-8589-b681cc3b02ac.mp4



# 📚 Rules of the game 📚 #

## 🔎 Objects 🔍 ##
The card has 4 types of objects.

* ![#ffffff](https://via.placeholder.com/15/ffffff/000000?text=+) The white box which represents the size of the grid
 
* ![#1FFF00](https://via.placeholder.com/15/1FFF00/000000?text=+) The agents, 2 in number, who must move to solve the level

* ![#0E7200](https://via.placeholder.com/15/0E7200/000000?text=+) Victory points, 2 in number, the agents will have to be simulatenement on a distinct victory point to solve the puzzle

* ![#FF0000](https://via.placeholder.com/15/FF0000/000000?text=+) The blockers, which will prevent the agent from going to a square that has a blocker


## 🕹️ Mechanics of movement 🕹️ ##

### 🦿 Displacement 🦿 ###

You can move in all four directions (⬅️⬆️⬇️➡️ ), the movement of the agents is synchronized, i.e. if you decide to go right, both agents will try to go right.

![2021-05-12 01-39-54](https://user-images.githubusercontent.com/53370597/117897609-b2494080-b2b2-11eb-9f04-3181c1a6789a.gif)

‼️ If a blocker is above agent 1 but agent 2 has nothing above it and you decide to go up, you will create an offset.

![2021-05-12 01-50-09](https://user-images.githubusercontent.com/53370597/117898223-03a5ff80-b2b4-11eb-9fca-832ab417dcfa.gif)

### 🕳️ Teleportation 🕳️ ###

The walls are like teleporters, if you are at the base edge of the map and you go to the bottom, you will be teleported from the top edge (if there is no blocker).
Also from right to left.

![2021-05-12 01-56-30](https://user-images.githubusercontent.com/53370597/117898630-efaecd80-b2b4-11eb-80c5-96da0b2c706b.gif)

# 🧱 Create your own level 🧱 #  ⚠️WORK IN PROGRESS⚠️

To start creating your own levels and try them out or have them tested by the agent, you must go to this link :

## How to create a level ❓
To create a level you must first choose the size of the grid in length and width (between 3 and 10), then place your obstacles / agents / victory points.

## How to launch the tests ❓

After validating your map 


Then you have the choice between two buttons : 🔘 `Your Run`  🔘 `Agent Run`.

# 🧠 Agent learning 🧠 #

## 💰 Reward 💰 ##
The reward is straightforward; at each step, I give him <b>0</b> rewards, and when he wins the game, he gets <b>1</b>.

I didn't want to go for more complex rewards, like, for example, one that would have given more rewards when the agent's pattern resembles the pattern of the victory points because you can switch from a highly different pattern to the solution, main thanks to teleportation. Moreover, I wanted to see how it would go after solving these puzzles without any additional indication.

## 👁️ Observation 👁️ ##

I faced the problem that the observation space must remain the same, but I want the agent to play on different types of terrain. 
I saw three methods that were available to me: 

* 👀 Use the sensor perceptions 👀
* 📷 Use the camera sensor 📷
* 📝 Give a array of size N that fills the rest of the map with -1 when it is empty 📝

I chose the painting, because it seemed more appropriate to the game.

The observations are : 

* Map Size -> X and Y
* Position Agent1 and Agent2 -> X1, Y1 and X2, Y2
* Position Victory1 and Victory2 -> X1, Y1 and X2, Y2
* Position of N blocker ([0,6]) -> Xn and Yn
* -1 to fill the array ([6-N] * 2)

Size of the observation = <b>16</b>

## 🦾 Action 🦾  ##

The actions are very simple, they are <b<4</b> discrete values, which define the <b>4</b> movements.

0 -> ⬆️
1 -> ⬇️ 
2 -> ➡️
3 -> ⬅️

## 📈 Result 📈 ##

Currently, my agent is much stronger than a human for puzzle solving. However, in some situations where a human would quickly see that the problem is solvable in 1 move, the agent does not see it and takes paths sometimes not optimized.
<div align="center">
 
![Capture](https://user-images.githubusercontent.com/53370597/120885199-94d07380-c5df-11eb-892f-bc33163348a9.PNG)
 
 </div>
