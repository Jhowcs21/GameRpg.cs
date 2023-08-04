using System;
using System.Numerics;
using System.Threading;

namespace NewProject.cs.Projeto
{
   

public class Program
    {
        private static List<Monstro> monstrosDisponiveis = new List<Monstro>
        {
            new Monstro("Zumbi", 1, TipoMonstro.Zumbi, TiposDeAfinidades.Agua, TiposDeAfinidades.Fogo),
            new Monstro("Esqueleto", 1, TipoMonstro.Esqueleto, TiposDeAfinidades.Pedra, TiposDeAfinidades.Agua),
            new Monstro("Lobo", 1, TipoMonstro.Lobo, TiposDeAfinidades.Escuridao, TiposDeAfinidades.Luz),
            new Monstro("Aranha", 1, TipoMonstro.Aranha, TiposDeAfinidades.Vento, TiposDeAfinidades.Pedra),
            new Monstro("Gigante", 1, TipoMonstro.Gigante, TiposDeAfinidades.Fogo, TiposDeAfinidades.Vento),
            new Monstro("Dragão", 1, TipoMonstro.Dragao, TiposDeAfinidades.Raio, TiposDeAfinidades.Escuridao)
        };

        //seilá
        public static void Main()
        {
            
            // Criação do personagem pelo jogador
            Personagens player = CriançãoDoPersonagem();

            // Variável para contar as vitórias do jogador
            int vitorias = 0;
            // Loop do jogo até o jogador desejar sair
            bool gameRunning = true;
            while (gameRunning)
            {
                // Verifica se o jogador já venceu 4 vezes para aparecer um Boss
                if (vitorias >= 4)
                {
                    Monstro boss = CreateBossMonster(player.Experience);
                    Batalha(player, boss);
                    vitorias = 0; // Reinicia o contador de vitórias
                }
                else
                {
                    Monstro monster = GetRandomMonster();
                    Batalha(player, monster);
                    vitorias++; // Incrementa o contador de vitórias
                }

                // Pós-batalha: recuperação de vida e passagem de nível
                player.Vida = player.VidaMaxima;
                player.Mana = player.ManaMaxima;
                player.Experience++;

                // Perguntar ao jogador se ele quer continuar o jogo
                Console.WriteLine("Deseja continuar jogando? (s/n)");
                string input = Console.ReadLine();
                gameRunning = (input.ToLower() == "s");
            }
        }

        // Função para criar o personagem pelo jogador
         static Personagens CriançãoDoPersonagem()
        {
            Personagens player = new Personagens("Alan", 10, RacaPersonagen.Anao, TipoClasse.Guerreiro, TiposDeAfinidades.Escuridao, TiposDeAfinidades.Raio);

            Console.WriteLine("Crie seu personagem:");
            Console.Write("Nome: ");
            player.Nome = Console.ReadLine();

            Console.WriteLine("Escolha uma classe:");
            foreach (TipoClasse characterClass in Enum.GetValues(typeof(TipoClasse)))
            {
                int indexClasse = (int)characterClass + 1; // Adiciona 1 ao índice numérico
                Console.WriteLine($"{indexClasse}. {characterClass}");
            }
            int escolhaClasse = GetValidChoice(Enum.GetValues(typeof(TipoClasse)).Length);
            player.Classe = (TipoClasse)escolhaClasse;

            Console.WriteLine("Escolha uma raça:");
            foreach (RacaPersonagen characterRace in Enum.GetValues(typeof(RacaPersonagen)))
            {
                int indexRaca = (int)characterRace + 1;
                Console.WriteLine($"{indexRaca}. {characterRace}");
            }
            int escolhaRaca = GetValidChoice(Enum.GetValues(typeof(RacaPersonagen)).Length);
            player.Raca = (RacaPersonagen)escolhaRaca;

            Console.WriteLine("Escolha uma afinidade:");
            foreach (TiposDeAfinidades characterElement in Enum.GetValues(typeof(TiposDeAfinidades)))
            {
                int indexElement = (int) characterElement + 1;
                Console.WriteLine($"{indexElement}. {characterElement}");
            }
            int elementChoice = GetValidChoice(Enum.GetValues(typeof(TiposDeAfinidades)).Length);
            player.Elemento = (TiposDeAfinidades)elementChoice;

            // Defina os atributos base do personagem de acordo com a raça e classe escolhidas (você pode ajustar os valores)
            player = new Personagens(player.Nome, 1, player.Raca, player.Classe, player.Elemento, TiposDeAfinidades.Pedra);

            return player;
        }
        
        // Função para criar um monstro aleatório com base no nível do jogador
        static Monstro CreateRandomMonster(int playerLevel)
        {
            Random random = new Random();

            TipoMonstro[] tiposMonstro = (TipoMonstro[])Enum.GetValues(typeof(TipoMonstro));
            TipoMonstro tipoMonstro = tiposMonstro[random.Next(tiposMonstro.Length)];

            // Gera um monstro aleatório com base no nível do jogador
            int level = playerLevel + random.Next(1, 4); // Ajuste o valor do intervalo para controlar o nível dos monstros gerados

            TiposDeAfinidades[] elementos = (TiposDeAfinidades[])Enum.GetValues(typeof(TiposDeAfinidades));
            TiposDeAfinidades element = elementos[random.Next(elementos.Length)];

            TiposDeAfinidades[] fraquezas = elementos.Where(x => x != element).ToArray();
            TiposDeAfinidades weakness = fraquezas[random.Next(fraquezas.Length)];

            

            Monstro boss = new Monstro($" Nível {level}", level, tipoMonstro, element, weakness);
            return boss;
        }

        // Função para criar um Boss com base na experiência do jogador
        static Monstro CreateBossMonster(int playerExperience)
        {
            Monstro boss = CreateRandomMonster(playerExperience * 2 + 5); // O Boss é 2.5 vezes mais forte que um monstro normal

            // Defina algumas características especiais do Boss aqui (opcional)

            return boss;
        }
        static Monstro GetRandomMonster()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, monstrosDisponiveis.Count);
            Monstro selectedMonster = monstrosDisponiveis[randomIndex].Clone(); // Faz uma cópia do monstro selecionado
            return monstrosDisponiveis[randomIndex];
        }
        static void ExibirStatus(Personagens player, Monstro enemy)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine($"{enemy.Nome}: {enemy.Vida} / {enemy.VidaMaxima}");
            Console.WriteLine($"{player.Nome}: {player.Vida} / {player.VidaMaxima} - Mana: {player.Mana} / {player.ManaMaxima}");
            Console.WriteLine("===============================================");
        }
        // Função para iniciar a batalha entre o personagem e o monstro
        static void Batalha(Personagens player, Monstro enemy)
        {
            bool battleRunning = true;
            Random random = new Random();
            Console.Clear();
            Console.WriteLine($"Um {enemy.Nome} apareceu! Prepare-se para a batalha!");

            while (battleRunning)
            {
                ExibirStatus(player, enemy);
                // Turno do jogador
                

                Console.WriteLine($"{enemy.Nome}: {enemy.Vida} / {enemy.VidaMaxima}");
                Console.WriteLine($"{player.Nome}: {player.Vida} / {player.VidaMaxima}");
                Console.WriteLine($"{player.Nome}, é sua vez de atacar!");
                Console.WriteLine("Escolha uma ação:");
                Console.WriteLine("1. Ataque básico");
                Console.WriteLine("2. Ataque especial");
                int actionChoice = GetValidChoice(2);

                if (actionChoice == 1)
                {
                    player.AtaqueBasico(enemy);
                }
                else
                {
                    player.AtaqueMagico(enemy);
                }

                // Turno do monstro
                if (enemy.Vida > 0)
                {
                    int randomAction = random.Next(1, 3);
                    if (randomAction == 1)
                    {
                        enemy.Atacar(player);
                    }
                    else
                    {
                        enemy.AtaqueEspecial(player);
                    }
                }

                // Verificar se a batalha terminou
                if (player.Vida <= 0 || enemy.Vida <= 0)
                {
                    battleRunning = false;
                }
            }

            // Exibir o resultado da batalha
            if (player.Vida > 0)
            {
                Console.WriteLine($"Você derrotou o {enemy.Nome}!");
            }
            else
            {
                Console.WriteLine($"Você foi derrotado pelo {enemy.Nome}!");
                Environment.Exit(0); // O jogo termina caso o jogador seja derrotado
            }
        }

        // Função auxiliar para obter uma escolha válida do jogador
        public static int GetValidChoice(int maxChoice)
        {
            int choice;
            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);

            while (!isValidChoice || choice < 1 || choice > maxChoice)
            {
                Console.WriteLine("Escolha inválida. Tente novamente.");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);
            }

            return choice;
        }
    }

}
