// SPDX-License-Identifier: MIT
pragma solidity >=0.8.0;


function positionToEntityKey(int32 x, int32 y) pure returns (bytes32) {
  return keccak256(abi.encode(x, y));
}

function position3DToEntityKey(int32 x, int32 y, int32 z) pure returns (bytes32) {
  return keccak256(abi.encode(x, y, z));
}

