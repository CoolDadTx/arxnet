# Todo List

## Must Haves

- [X] Add SFML support
- [X] Add Glu support
- [X] Replace C++ file logic 
- [X] Replace iostream logic
- [X] Compile cleanly (minus obsoletes and styling warnings)

## Architecture

- [ ] Break out `GlobalMembers` in each file (and child types) into separate types based upon area
- [ ] Create interface for sound so it can be replaced
- [ ] Create interface for graphics so it can be replaced
- [ ] Create a logging interface to allow logging info/warning/debug messages to console
- [ ] Replace magic ints with boolean or enums as needed
- [ ] Convert public fields to properties
- [ ] Get rid of global functions
- [ ] Remove reliance on converter array logic

## Enhancements

- [ ] Switch to Winforms to allow hiding of console? [OpenTK](https://github.com/mono/opentk/blob/master/Source/Examples/OpenTK/GLControl/GLControlGameLoop.cs) [SFML](https://en.sfml-dev.org/forums/index.php?topic=9141.0)
- [ ] Make Scenarios a standalone type
- [ ] Make Items a standalone type
- [ ] Make Spells a standalone type
- [ ] Make Encounters a standalone type
- [ ] Make Modules a standalone type
- [ ] Make Guilds/Factions a standalone type
- [ ] Make an Effect class and allow adding to player
- [ ] Create a game clock to handle timing logic
- [ ] Implement game loop with hooks so we can separate logic out as needed
- [ ] Make load/save flexible to support versioned data (perhaps JSON because files aren't too big)
- [ ] Implement event-style system for reacting to important events to decouple the code
- [ ] Support unlimited # of items, spells, etc
- [ ] Add extensibility points for adding new stuff (items, spells, etc)
- [ ] Normalize music/graphics files so the various filename/path logic can be simplified
- [ ] DirectX or Vulkan support?
- [ ] Expand support for resolutions
- [ ] Move all hard coded lists of stuff to data files
- [ ] Make menu logic a standalone class that can be instantiated and used via delegates on demand
- [ ] Lazy load data such as items so we can speed up the game (use a cache to prevent it from growing out of control)
- [ ] Add JSON support for files to make it easier to manage