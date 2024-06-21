# Godot-4-Card-Game-CSharp
Following tutorial but also converting everything from GDScript to C# - https://youtube.com/playlist?list=PL6SABXRSlpH8CD71L7zye311cp9R4JazJ&amp;si=UyhBcNptWNp4eNCd

# High-Level Architecture

```mermaid
graph TD
    Battle -->|start_battle()| EnemyHandler
    Battle -->|turn management| PlayerHandler
    Battle -->|win/lose conditions| Player
    Battle -->|turn management| UI

    EnemyHandler -->|enemy turn management| Enemy1
    EnemyHandler -->|doing enemy actions| Enemy2

    PlayerHandler -->|player turn management| Player
    PlayerHandler -->|drawing/discarding cards| Player

    Player -->|displaying stats| UI
    Player -->|taking damage visually| UI

    UI -->|cards| UI
    UI -->|mana| UI
    UI -->|tooltips| UI

# Data Management: Resources

```mermaid
graph TD
    Card -->|name| 
    Card -->|mana cost| 
    Card -->|effect| 

    CardPile -->|array of cards| 
    CardPile -->|used decks, the draw pile, and the discard pile| 

    Effect -->|do something to a target| 
    Effect -->|add block| 
    Effect -->|damage| 

    Stats -->|enemy turn management| CharacterStats
    Stats -->|doing enemy actions| CharacterStats
    Stats -->|etc.| CharacterStats

    CharacterStats --> EnemyStats

    Intent -->|icon| 
    Intent -->|text| 
