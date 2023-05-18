// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;
import { System } from "@latticexyz/world/src/System.sol";
import { IStore } from "@latticexyz/store/src/IStore.sol";
// TODO: import tables
import { addressToEntityKey } from "../addressToEntityKey.sol";

contract MoveSystem is System {
	function move(int32 x, int32 y) public {
			 // Get player key
	    bytes32 player = addressToEntityKey(address(_msgSender()));

			// TODO: Get position for player

	    // TODO: check if there is a player at the position

			// TODO: Set position
	}

		function spawn(int32 x, int32 y) public {
    bytes32 player = addressToEntityKey(address(_msgSender()));
		// TODO: Check if player has already spawned

		// TOOD: make sure position isn't occupied

		// TODO: set components for our player
  }
}