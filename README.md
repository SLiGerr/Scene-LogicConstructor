# Scene-LogicConstructor

+ **What?** - Easy scene template placer 
+ **Why?** 
    - To keep scenes more clean
    - To make sure that ui/managers/etc are same on all scenes that requires so 
    - Nice to have when you have many levels with same logic (Like Gameplay levels or something)
+ **How it works?** - It just spawns prefabs of the preselected logic when game starts

> [!NOTE]
> Execution order of placer is **-100** 

## Download

1) Open ```Package Manager``` in Unity, 
2) Select ```Add package from git URL...```,
3) Paste ```https://github.com/SLiGerr/Scene-LogicConstructor.git``` and press **Add** 

## Usage

<!-- > TLDR : **Tools/Scene-LogicConstructor/Add Logic Placer** -->

1) Create ```Scene Template``` by pressing ```Create Scene-LogicContainer``` in any folder you like.
2) Add ```Logic Placer``` to scene, by pressing ```Tools/Scene-LogicConstructor/Add Logic Placer``` or manually add component to new GameObject (if you want multiple placers).
3) Fill ```Logic``` field in LogicPlacer and you good to go!

> [!NOTE]
> **Tips:**<br/>
> 1) You can use multiple placers with different logic presets on same scene.
> 2) You can change name of the logic root parent in ```Logic Placer```.
> 3) You can disable parenting of the logic inside ```Template config```.
> 4) You can receive ```OnConstruction``` callback by adding ```ISceneConstruction``` to scripts

> [!WARNING]
> For ```OnConstruction``` callback to work, at least one of placers must be on scene.

## TODO:
- [ ] Make ```OnConstruction``` callback work without any placers too
