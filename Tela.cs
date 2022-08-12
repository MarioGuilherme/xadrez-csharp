using TabuleiroNM;
using Xadrez;

namespace xadrez_console {
    class Tela {
        public static void imprimirTabuleiro(Tabuleiro tabuleiro) {
            for (int i = 0; i < tabuleiro.Linhas; i++) {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                    if (tabuleiro.Peca(i, j) == null)
                        Console.Write("- ");
                    else {
                        ImprimirPeca(tabuleiro.Peca(i, j));
                        Console.Write(" ");
                    }
                Console.WriteLine();
            }
                Console.WriteLine("  a b c d e f g h");
        }

        public static PosicaoXadrez LerPosicaoXadrez() {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1].ToString());
            return new PosicaoXadrez(coluna, linha);

        }

        public static void ImprimirPeca(Peca peca) {
            if (peca.Cor == Cor.Branca)
                Console.Write(peca);
            else {
                ConsoleColor consoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = consoleColor;
            }
        }
    }
}