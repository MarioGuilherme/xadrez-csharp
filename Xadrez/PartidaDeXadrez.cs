using TabuleiroNM;

namespace Xadrez {
    class PartidaDeXadrez {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;
        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tabuleiro = new(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new();
            Capturadas = new();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca peca = Tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                Capturadas.Add(pecaCapturada);

            // #Jogada Especial - Roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
            }

            // #Jogada Especial - Roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(origemTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, destinoTorre);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca peca = Tabuleiro.RetirarPeca(destino);
            peca.DecrementarQuantidadeMovimentos();
            if (pecaCapturada != null) {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(peca, origem);

            // #Jogada Especial - Roque pequeno
            if (peca is Rei && destino.Coluna == origem.Coluna + 2) {
                Posicao origemTorre = new(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna + 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.DecrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
            }

            // #Jogada Especial - Roque grande
            if (peca is Rei && destino.Coluna == origem.Coluna - 2) {
                Posicao origemTorre = new(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new(origem.Linha, origem.Coluna - 1);
                Peca torre = Tabuleiro.RetirarPeca(destinoTorre);
                torre.IncrementarQuantidadeMovimentos();
                Tabuleiro.ColocarPeca(torre, origemTorre);
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Xeque = EstaEmXeque(Adversaria(JogadorAtual));

            if (TesteXequeMate(Adversaria(JogadorAtual)))
                Terminada = true;
            else {
                Turno++;
                MudarJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao posicao) {
            if (Tabuleiro.Peca(posicao) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (JogadorAtual != Tabuleiro.Peca(posicao).Cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");

            if (!Tabuleiro.Peca(posicao).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void MudarJogador() {
            JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) {
            HashSet<Peca> auxiliar = new();
            foreach (Peca peca in Capturadas)
                if (peca.Cor == cor)
                    auxiliar.Add(peca);
            return auxiliar;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor) {
            HashSet<Peca> auxiliar = new();
            foreach (Peca peca in Pecas)
                if (peca.Cor == cor)
                    auxiliar.Add(peca);
            auxiliar.ExceptWith(PecasCapturadas(cor));
            return auxiliar;
        }

        private Cor Adversaria(Cor cor) {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca Rei(Cor cor) {
            foreach (Peca peca in PecasEmJogo(cor))
                if (peca is Rei)
                    return peca;
            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca rei = Rei(cor);

            foreach (Peca peca in PecasEmJogo(Adversaria(cor))) {
                bool[,] matriz = peca.MovimentosPossiveis();
                if (matriz[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor) {
            if (!EstaEmXeque(cor))
                return false;

            foreach (Peca peca in PecasEmJogo(cor)) {
                bool[,] matriz = peca.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                        if (matriz[i, j]) {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
            }

            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca) {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas() {
            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca));


            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta));
        }
    }
}