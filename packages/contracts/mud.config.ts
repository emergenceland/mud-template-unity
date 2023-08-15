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
    BytesTest: "bytes32",
    State: "StateType",
    Inflate: "uint32",
    
  },
  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Counter")],
    },
  ],
});
