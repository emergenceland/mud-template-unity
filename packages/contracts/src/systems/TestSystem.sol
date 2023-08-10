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

  }

  function spawnBall(int32 ball) public {
    bytes32 key = keccak256(abi.encode(ball));
    Tester.set(key, true);
    State.set(key, StateType.Normal);
  }

  function nextState(int32 ballNumber) public {
    bytes32 ball = keccak256(abi.encode(ballNumber));
    uint currentType = uint(State.get(ball));
    currentType = (currentType + 1) % uint(StateType.Count);
    State.set(ball, StateType(currentType));
  }

  function deleteState() public {}

  function setState() public {}
}
