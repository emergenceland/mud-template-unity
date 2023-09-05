// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { IStore } from "@latticexyz/store/src/IStore.sol";
import { Counter } from "../codegen/Tables.sol";
import { Tester, State, Inflate, AllTypes } from "../codegen/Tables.sol";
import { StateType } from "../codegen/Types.sol";
import { addressToEntityKey } from "../utility/addressToEntityKey.sol";

contract TestSystem is System {

  function startTest() public {
    bytes32 player = addressToEntityKey(address(_msgSender()));
    int256[2] memory staticTest;
    staticTest[0] = 3;
    staticTest[1] = 4;
    uint256[] memory dynamicTest = new uint256[](3);
    dynamicTest[0] = 5;
    dynamicTest[1] = 6;
    dynamicTest[2] = 7;
    bool[] memory emptyTest = new bool[](0);
    
    AllTypes.set(player, true, int32(-3), uint32(5), uint256(50), StateType.Happy, player);
    // AllTypes.set(player, true, int32(-3), uint32(5), int256(-40), uint256(50), StateType.Happy, player);
    // AllTypes.set(player, true, -3, 5, -40, 50, StateType.Happy, player, staticTest, dynamicTest, emptyTest);
    
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
