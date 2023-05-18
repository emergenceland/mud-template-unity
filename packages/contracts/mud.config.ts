import { mudConfig, resolveTableId } from "@latticexyz/world/register";

export default mudConfig({
  overrideSystems: {
    IncrementSystem: {
      name: "increment",
      openAccess: true,
    },
  },
  tables: {
    Counter: {
      schema: {
        value: "uint32",
      },
      storeArgument: true,
    },
    /*
     * TODO:
     * - Position: (x: uint32, y: uint32),
     * - Player: bool,
     * - Health: uint32,
     * - Damage: uint32
     */
  },
  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Counter")],
    },
    // TODO: Add reverse lookup for Position
  ],
});
