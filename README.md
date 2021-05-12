# 🧩 Built puzzles for my Agent  🧩 #

In this project you will be able to create levels for my Agent and see how he can solve them, you also have the possibility to test the levels yourself to compare your score to the Agent's ! 

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

# 🧱 Create your own level 🧱 #

To start creating your own levels and try them out or have them tested by the agent, you must go to this link :

## How to create a level ❓
To create a level you must first choose the size of the grid in length and width (between 1 and 10), then place your obstacles / agents / victory points.

## How to launch the tests ❓

After validating your map 


Then you have the choice between two buttons : 🔘 `Your Run`  🔘 `Agent Run`.

# 🧠 Agent learning 🧠 #

## 💰 Reward 💰 ##


## 👁️ Observation 👁️ ##

## 🦾 Action 🦾  ##

## 🥇 Result 🥇 ##
