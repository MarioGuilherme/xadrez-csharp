namespace TabuleiroNM {
    abstract class Peca {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor) {
            Cor = cor;
            QuantidadeMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarQuantidadeMovimentos() {
            QuantidadeMovimentos++;
        }

        public void DecrementarQuantidadeMovimentos() {
            QuantidadeMovimentos--;
        }

        public bool ExisteMovimentosPossiveis() {
            bool[,] matriz = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                    if (matriz[i, j])
                        return true;

            return false;
        }

        public bool PodeMoverPara(Posicao posicao) {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}