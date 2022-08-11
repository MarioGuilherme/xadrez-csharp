using TabuleiroNM;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {
            Tabuleiro tabuleiro = new(8, 8);
            Tela.imprimirTabuleiro(tabuleiro);
        }
    }
}