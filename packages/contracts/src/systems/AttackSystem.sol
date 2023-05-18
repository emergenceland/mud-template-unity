// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { getKeysWithValue } from "@latticexyz/world/src/modules/keyswithvalue/getKeysWithValue.sol";
// TODO: import tables
import { addressToEntityKey } from "../addressToEntityKey.sol";

contract AttackSystem is System {
  function attack(int32 x, int32 y) public {
    bytes32 player = addressToEntityKey(address(_msgSender()));
		// TODO: Get the player at position x, y

    // TODO: get all coords surrounding the target (including the target)

    // TODO: iterate over all coords surrounding the target
  }

  function attackTarget(bytes32 player, bytes32[] memory atPosition) internal {
    bytes32 defender = atPosition[0];

		// TODO: Make sure target isn't dead and is a player

		// TODO: Get player damage and defender health
		// TODO: Check if defender is dead
		// TODO: Update health
    
  }

// TODO: Uncomment once you have the Position Table set up
// function mooreNeighborhood(PositionData memory center) internal pure returns (PositionData[] memory) {
//     PositionData[] memory neighbors = new PositionData[](9);
//     uint256 index = 0;

//     for (int32 x = -1; x <= 1; x++) {
//         for (int32 y = -1; y <= 1; y++) {
//             neighbors[index] = PositionData(center.x + x, center.y + y);
//             index++;
//         }
//     }

//     return neighbors;
// }
}
