# Todo List

## Must Haves

- [ ] Add SFML support
- [ ] Add Glex support
- [ ] Replace C++ file logic 
- [ ] Replace iostream logic
- [ ] Compile cleanly

## Architecture

- [ ] Create interface for sound so it can be replaced
- [ ] Create interface for graphics so it can be replaced
- [ ] Create a logging interface to allow logging info/warning/debug messages to console
- [ ] Replace magic ints with boolean or enums as needed
- [ ] Convert public fields to properties
- [ ] Get rid of global functions
- [ ] Remove reliance on converter array logic
- [ ] Normalize binary file handling so it can be reused

## Enhancements

- [ ] Implement game loop with hooks so we can separate logic out as needed
- [ ] Make load/save flexible to support versioned data (perhaps JSON because files aren't too big)
- [ ] Implement event-style system for reacting to important events to decouple the code
- [ ] Support unlimited # of items, spells, etc
- [ ] Add extensibility points for adding new stuff (items, spells, etc)
- [ ] DirectX support
- [ ] Expand support for resolutions
- [ ] Make guilds a standalone class
- [ ] Move all hard coded lists of stuff to data files
- [ ] Make menu logic a standalone class that can be instantiated and used via delegates on demand
- [ ] Make spells a standalone class with derived types (or just data) for each unique spell
- [ ] Make encounters a standalone class with derived types (or just data) for each unique encounter
- [ ] Make City, Dungeon, Arena separate modules that can be snapped into place
- [ ] Make items a standalone class with data for each unique item
- [ ] Lazy load data such as items so we can speed up the game (use a cache to prevent it from growing out of control)