import { mudConfig, resolveTableId } from "@latticexyz/world/register";

export default mudConfig({
  overrideSystems: {
    IncrementSystem: {
      name: "increment",
      openAccess: true,
    },
  },

  //create enum types here
  enums: {
    StateType: ["Normal", "Happy", "Sad", "Count"],
  },

  tables: {

    //used in the counter example scene
    Counter: {
      schema: {
        value: "uint32",
      },
      storeArgument: true,
    },


    //used in test scenes 
    Tester: "bool",
    BytesTest: "bytes32",
    State: "StateType",
    Inflate: "uint32",
    AllTypes: {
      dataStruct: false,
      schema: {
        boolTest: "bool",
        int32Test: "int32",
        uint32Test: "uint32",
        // bigIntTest: "int256",
        bigUintTest: "uint256",
        enumTest: "StateType",
        entityTest: "bytes32",
        // staticArrayTest: "int256[2]",
        // dynamicArrayTest: "uint256[]",
        // emptyArrayTest: "bool[]",
      },
    },

  },
  modules: [
    {
      name: "KeysWithValueModule",
      root: true,
      args: [resolveTableId("Counter")],
    },
  ],
});
