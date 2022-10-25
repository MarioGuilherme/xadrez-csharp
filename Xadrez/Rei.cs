using TabuleiroNM;

namespace Xadrez {
    class Rei : Peca {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor) {}

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao posicao) {
            Peca peca = Tabuleiro.Peca(posicao);
            return peca == null || peca.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] movimentosPossiveis = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao posicao = new(0, 0);

            // Acima
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Nordeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Direita
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Abaixo
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Sudoeste
            posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Esquerda
            posicao.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            // Noroeste
            posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
                movimentosPossiveis[posicao.Linha, posicao.Coluna] = true;

            return movimentosPossiveis;
        }
    }
}