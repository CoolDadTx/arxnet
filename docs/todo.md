# Todo List

## Must Haves

- [ ] Add SFML support
- [ ] Replace C++ file logic 
- [ ] Replace iostream logic
- [ ] Compile cleanly

## Architecture

- [ ] Create interface for sound so it can be replaced
- [ ] Create interface for graphics so it can be replaced
- [ ] Create a logging interface to allow logging info/warning/debug messages to console
- [ ] Move all hard coded lists of stuff to data files
- [ ] Make menu logic a standalone class that can be instaniated and used via delegates on demand
- [ ] Make spells a standalone class with derived types (or just data) for each unique spell
- [ ] Make encounters a standalone class with derived types (or just data) for each unique encounter
- [ ] Make City, Dungeon, Arena separate modules that can be snapped into place
- [ ] Make items a standalone class with data for each unique item

## Enhancements

- [ ] Implement game loop with hooks so we can separate logic out as needed
- [ ] Make load/save flexible to support versioned data (perhaps JSON because files aren't too big)
- [ ] Implement event-style system for reacting to important events to decouple the code
- [ ] Support unlimited # of items, spells, etc
- [ ] Add extensibility points for adding new stuff (items, spells, etc)
- [ ] OpenGL support
- [ ] DirectX support