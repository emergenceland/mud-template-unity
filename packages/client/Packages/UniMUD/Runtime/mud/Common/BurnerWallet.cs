using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using UnityEngine;

namespace mud
{
    public static partial class Common
    {
        public static string GeneratePrivateKey()
        {
            var ecKey = EthECKey.GenerateKey();
            return ecKey.GetPrivateKeyAsBytes().ToHex();
        }

        public static string GetBurnerPrivateKey()
        {
            {
                var savedBurnerWallet = PlayerPrefs.GetString("burnerWallet");
                if (!string.IsNullOrWhiteSpace(savedBurnerWallet))
                {
                    return savedBurnerWallet;
                }

                // TODO: Insecure
                var newPrivateKey = GeneratePrivateKey();
                PlayerPrefs.SetString("burnerWallet", newPrivateKey);
                PlayerPrefs.Save();
                return newPrivateKey;
            }
        }
    }
}
