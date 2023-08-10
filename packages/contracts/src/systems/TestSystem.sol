// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { IStore } from "@latticexyz/store/src/IStore.sol";
import { Counter } from "../codegen/Tables.sol";
import { Tester, State } from "../codegen/Tables.sol";
import { StateType } from "../codegen/Types.sol";
import { addressToEntityKey } from "../utility/addressToEntityKey.sol";

contract TestSystem is System {
  function startTest() public {

    for (int32 i = 0; i < 5; i++) {
      bytes32 key = keccak256(abi.encode(i));
      Tester.set(key, true);
      State.set(key, StateType.Normal);
    }

  }

  function nextState() public {
    bytes32 ball = addressToEntityKey(address(_msgSender()));
    uint currentType = uint(State.get(ball));
    currentType = (currentType + 1) % uint(StateType.Count);
    State.set(ball, StateType(currentType));
  }

  function deleteState() public {}

  function setState() public {}
}
