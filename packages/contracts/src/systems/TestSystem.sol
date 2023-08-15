// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { IStore } from "@latticexyz/store/src/IStore.sol";
import { Counter } from "../codegen/Tables.sol";
import { Tester, State, Inflate } from "../codegen/Tables.sol";
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

  function spawnBall(int32 ball) public {
    bytes32 key = keccak256(abi.encode(ball));
    Tester.set(key, true);
    State.set(key, StateType.Normal);
    Inflate.set(key, 0);
  }

  function setSimple(bytes32 ball) public {
    uint currentType = uint(State.get(ball));
    currentType = (currentType + 1) % uint(StateType.Count);
    State.set(ball, StateType(currentType));

    uint32 inflate = Inflate.get(ball);
    Inflate.set(ball, inflate+1);
  }

  function deleteSimple(bytes32 ball) public {

    State.deleteRecord(ball);

    uint32 inflate = Inflate.get(ball);
    Inflate.set(ball, inflate+1);
  }

  function setDelete(bytes32 ball) public {
    uint currentType = uint(State.get(ball));
    currentType = (currentType + 1) % uint(StateType.Count);
    State.set(ball, StateType(currentType));
    State.deleteRecord(ball);

    uint32 inflate = Inflate.get(ball);
    Inflate.set(ball, inflate+1);
  }

  function deleteSet(bytes32 ball) public {
    uint currentType = uint(State.get(ball));
    currentType = (currentType + 1) % uint(StateType.Count);
    State.deleteRecord(ball);
    State.set(ball, StateType(currentType));

    uint32 inflate = Inflate.get(ball);
    Inflate.set(ball, inflate+1);
  }
}
