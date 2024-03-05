// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("yMsv6KCniBt96tfWlKlDvZ8EQWkKQWRGrtxm9kC6qcsQPSwinqUMMysvUO6YnGHQ4bubiDGibgHkKgU0GZqUm6sZmpGZGZqamz3oKiY0VU09kVxXodYhzapw0bH0lDnl/CG/OzfxpPenQHfIZk8BzwYh9o3zHZgMrAIWtxXnV6tzdfDP8HK96xaVV0mrGZq5q5adkrEd0x1slpqamp6bmB+BmYH37ARaLgjssVrro2egVWHcZ2JOQG3Q2RTucL4lq90iEYaoPfXcU2p/m8qmIsR1cbRIWqEpDMuSzHGYk1t1IzMuuQBiGooo32V19pFU6HokcO85mutwW5TZNyE6HwQnIT0k8HpbUtK1fCB3eVsqg/uAP74U59Koi+FXes2UqpmYmpua");
        private static int[] order = new int[] { 4,3,9,3,12,10,10,12,12,11,12,12,12,13,14 };
        private static int key = 155;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
