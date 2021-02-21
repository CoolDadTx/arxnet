# Todo List

## Must Haves

- [X] Add SFML support
- [X] Add Glu support
- [X] Replace C++ file logic 
- [X] Replace iostream logic
- [X] Compile cleanly (minus obsoletes and styling warnings)

## Issues

- [X] Freezes at create char counter screen, counters are not moving
- [ ] OpenGL settings don't seem to init right but this is the same behavior as the native code
- [ ] Background images are rendered but not buildings
- [ ] Attack menu not working
- [ ] Quit or clicking X on window does not display main menu

## Architecture

Interesting [read](https://gameprogrammingpatterns.com)

- [ ] OpenTK 4.x completely changed how rendering works making existing code incompatible for now. Sticking with 3.x
- [ ] Break out `GlobalMembers` in each file (and child types) into separate types based upon area
- [ ] Create interface for sound so it can be replaced
- [ ] Create interface for graphics so it can be replaced
- [ ] Create a logging interface to allow logging info/warning/debug messages to console
- [ ] Replace magic ints with boolean or enums as needed
- [ ] Convert public fields to properties
- [ ] Get rid of global functions
- [ ] Remove reliance on converter array logic
- [ ] Move `ini` settings to config file

## Enhancements

- [ ] ?Switch to Winforms to allow hiding of console? [OpenTK](https://github.com/mono/opentk/blob/master/Source/Examples/OpenTK/GLControl/GLControlGameLoop.cs) [SFML](https://en.sfml-dev.org/forums/index.php?topic=9141.0)
- [ ] ?Switch to WPF [OpenTK](https://github.com/jayhf/OpenTkControl)
- [ ] Define the core [components](https://gameprogrammingpatterns.com/component.html) such as `InputComponent`, `GraphicsComponent`, etc that is tied to a `GameObject` base clss
- [ ] Make Scenarios a standalone type
- [ ] Make Items a standalone type
- [ ] Make Spells a standalone type (maybe a sandbox class with all the core functionality that is used to build up each one)
- [ ] Make Encounters a standalone type (consider a single `Monster` class and a `Breed` class [?](https://gameprogrammingpatterns.com/type-object.html))
- [ ] Make Modules a standalone type
- [ ] Make Guilds/Factions a standalone type
- [ ] Make an Effect class and allow adding to player (maybe a sandbox class with all the core functionality that is used to build up each one)
- [ ] Create a game clock to handle timing logic
- [ ] Need a real game loop instead of the various mixing of events and rendering (https://gameprogrammingpatterns.com/game-loop.html)
- [ ] Implement "active objects" list that stores objects that need to be updated each loop, needs to handle adds/subs while in the loop. `Update` method should be on component instead of the root [entity](https://gameprogrammingpatterns.com/update-method.html).
- [ ] Make load/save flexible to support versioned data (perhaps JSON because files aren't too big)
- [ ] Create `InputManager` to handle input for user
- [ ] Add support for key bindings (consider command pattern so we can swap out based upon binding - probably need to pass actor or game state to command, maybe a new instance each time so we can track state and undo commands that cann't be done)
- [ ] Create `Screen` concept to handle various screens
- [ ] Support unlimited # of items, spells, etc
- [ ] Normalize music/graphics files so the various filename/path logic can be simplified (maybe introdude a `FileSystem` class that has the base path set up and normalize the filenames)
- [ ] DirectX or Vulkan support?
- [ ] Expand support for resolutions and normalize access (Atari, new/old textures, size, etc)
- [ ] Move all hard coded lists of stuff to data files
- [ ] Make menu logic a standalone class that can be instantiated and used via delegates on demand
- [ ] Lazy load data such as items so we can speed up the game (use a cache to prevent it from growing out of control)
- [ ] Add JSON support for files to make it easier to manage refer to [this](https://gameprogrammingpatterns.com/prototype.html) for an interesting "prototype" idea to reduce data dups
- [ ] Consider adding state class(es) to handle switch transitions such as attack, defend, shop, etc. Also useful for transitioning graphics/sound/etc
- [ ] Move sound (and maybe others) to an event queue so it doesn't hold up game and we can handle multiple sounds at once without issues
- [ ] Sounds should not freeze screen until completed