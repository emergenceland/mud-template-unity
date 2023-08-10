import { mudConfig, resolveTableId } from "@latticexyz/world/register";

export default mudConfig({
  overrideSystems: {
    IncrementSystem: {
      name: "increment",
      openAccess: true,
    },
  },

  enums: {
    StateType: ["Normal", "Happy", "Sad", "Count"],
  },

  tables: {
    Counter: {
      schema: {
        value: "uint32",
      },
      storeArgument: true,
    },

    Tester: "bool",
    State: "StateType",
    
  },
  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Counter")],
    },
  ],
});
