using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewProject.cs.Projeto
{
    //Uso mas nem sei oque é direito!
   enum RacaPersonagen
    {
        Humanos,
        Fadas,
        Druidas,
        Anao,
        Elfo
    }

    enum TipoClasse
    {
        Guerreiro,
        Arqueiro,
        Assassino,
        Sacerdote,
        Feiticeiro,
        Monge
    }

    //Elementos
    enum TiposDeAfinidades
    {
        Fogo,
        Agua,
        Pedra,
        Vento,
        Raio,
        Escuridao,
        Luz

    }
    internal class Personagens
    {
        
        public string Nome { get;  set; }
        public int Nivel { get; set; } 
        public int Forca { get; set; }
        public int Defesa { get; set; }
        public int Inteligencia { get; set; }
        public int Agilidade { get; set; }
        public int Vida { get;  set; } 
        public int Mana { get;  set; }
        public int VidaMaxima { get; set; }
        public int ManaMaxima { get; set; }
        public int Experience { get; set; }


        public Dictionary<int, string> Attacks { get; set; }
        public TipoClasse Classe { get; set; }
        public RacaPersonagen Raca { get; set; }
        public TiposDeAfinidades Elemento { get; set; }
        public TiposDeAfinidades Fraqueza { get; set; }

        private static readonly Random random = new Random();

        // Vida e Mana recuperadas ao Subir de Nivel
        private int vidaRecuperadaPorNivel = 10;
        private int manaRecuperadaPorNivel = 5;

        public Personagens(string nome, int nivel, RacaPersonagen raca, TipoClasse tipoClasse, TiposDeAfinidades elemento, TiposDeAfinidades fraqueza) 
        {
            Nome = nome;
            Nivel = nivel;
            Raca = raca;  
             
            Elemento = elemento;
            Elemento = fraqueza;


            
            switch(tipoClasse)
            {
                    //Guerreiro
                case TipoClasse.Guerreiro:
                    Forca = 110 + nivel * 3;
                    Defesa = 50 + nivel * 2;
                    Inteligencia = 10 + nivel;
                    Agilidade = 30 + nivel;
                    VidaMaxima = 100 + nivel * 25;
                    ManaMaxima = 100 + nivel * 25;
                    break;
                     //Monge       
                case TipoClasse.Monge:
                    Forca = 50 + nivel * 3;
                    Defesa = 100 + nivel * 2;
                    Inteligencia = 30 + nivel;
                    Agilidade = 20 + nivel;
                    VidaMaxima = 100 + nivel * 30;
                    ManaMaxima = 100 + nivel * 20;
                    break;
                    //Arqueiro
                case TipoClasse.Arqueiro:
                    Forca = 50 + nivel * 3;
                    Defesa = 30 + nivel * 2;
                    Inteligencia = 20 + nivel;
                    Agilidade = 100 + nivel;
                    VidaMaxima = 100 + nivel * 20;
                    ManaMaxima = 100 + nivel * 30;
                    break;
                    //Assassino
                case TipoClasse.Assassino:
                    Forca = 20 + nivel * 3;
                    Defesa = 10 + nivel * 2;
                    Inteligencia = 5 + nivel;
                    Agilidade = 90 + nivel;
                    VidaMaxima = 100 + nivel * 15;
                    ManaMaxima = 100 + nivel * 35;
                    break;
                    //Feiticeiro
                case TipoClasse.Feiticeiro:
                    Forca = 10 + nivel * 3;
                    Defesa = 20 + nivel * 2;
                    Inteligencia = 110 + nivel;
                    Agilidade = 20 + nivel;
                    VidaMaxima = 100 + nivel * 5;
                    ManaMaxima = 100 + nivel * 45;
                    break;
                    //Sacerdote
                case TipoClasse.Sacerdote:
                    Forca = 15 + nivel * 3;
                    Defesa = 10 + nivel * 2;
                    Inteligencia = 90 + nivel;
                    Agilidade = 7 + nivel;
                    VidaMaxima = 100 + nivel * 10;
                    ManaMaxima = 100 + nivel * 40;
                    break;
            }
            Vida = VidaMaxima;
            Mana = ManaMaxima;
        }

        // Subir de Nivel e ganho de experiencia

        private void GanharExp(int experiencePoints)
        {
            Experience += experiencePoints;

            // Verificar se o personagem subiu de nível (você pode ajustar os requisitos de experiência para subir de nível)
            int requiredExperience = Nivel * 10;
            if (Experience >= requiredExperience)
            {
                LevelUp();
            }
        }
                
        public void LevelUp()
        {
            Nivel++;
            Vida = VidaMaxima + (Nivel * vidaRecuperadaPorNivel);
            Mana = ManaMaxima + (Nivel * manaRecuperadaPorNivel);
            Forca += 5;
            Defesa += 3;
            Inteligencia += 2;
            Agilidade += 2;

            Console.WriteLine($"{Nome} subiu para o nível {Nivel}!");
        }
        //Final do EXP

        private int CalculaDano(int attackerStrength, int defenderDefense)
        {
            int damage = attackerStrength - defenderDefense;
            return Math.Max(damage, 0);
        }

        private int CalculaAfinidadeDoDano(TiposDeAfinidades tiposDeAfinidades, TipoMonstro monsterType)
        {
            // Defina as fraquezas e afinidades dos elementos
            const int normalDamage = 10;
            const int affinityDamage = 20;
            const int weaknessDamage = 5;

            switch (tiposDeAfinidades)
            {
                case TiposDeAfinidades.Fogo:
                    return monsterType == TipoMonstro.Gigante ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Agua:
                    return monsterType == TipoMonstro.Zumbi ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Pedra:
                    return monsterType == TipoMonstro.Esqueleto ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Vento:
                    return monsterType == TipoMonstro.Aranha ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Raio:
                    return monsterType == TipoMonstro.Dragao ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Escuridao:
                    return monsterType == TipoMonstro.Lobo ? weaknessDamage : affinityDamage;
                case TiposDeAfinidades.Luz:
                    return monsterType == TipoMonstro.Lobo ? affinityDamage : normalDamage;
                default:
                    return normalDamage;
            }
        }

        private void VerificaSeOMonstroEstaMorto(Monstro monster)
        {
            if (monster.Vida <= 0)
            {
                Console.WriteLine($"{monster.Nome} foi derrotado!");
                GanharExp(5); // Ganha 5 de experiência por derrotar um monstro
            }
        }


        //Sofrer Dano
        public void SofreDano(int dano)
        {
            Vida -= Math.Max(dano - Defesa, 0);
            if( Vida <= 0 )
            {
                Console.WriteLine($"{Nome} foi derrotado!"  );
                Environment.Exit(0);
            }
        }

        //Metodos referentes ao ataque eo critico
        public void AtaqueBasico(Monstro monstro)
        {
            
            int dano = Forca + random.Next(-2, 3);
            int danoCausado = Math.Max(dano - monstro.Defesa, 0);
            monstro.Vida -= danoCausado;
            Console.WriteLine($"Você causou {danoCausado} de dano ao {monstro.Nome}!");
            
        }
        //Ataque Magico
        public void AtaqueMagico(Monstro monstro)
        {
            int custoMana = 15;
            if (Mana < custoMana)
            {
                Console.WriteLine("Mana insuficiente para usar o ataque mágico!");
                return;
            }
            int dano = Inteligencia + random.Next(5, 8);
            int danoCausado = Math.Max(dano - monstro.Defesa, 0);
            monstro.Vida -= danoCausado;
            Mana -= custoMana;
            Console.WriteLine($"Você usou o ataque mágico e causou {danoCausado} de dano ao {monstro.Nome}!");
        }

        public void AddAttack(int attackNumber, string attackName)
        {
            Attacks.Add(attackNumber, attackName);
        }

        //Chance do ataque ser critico
        private bool IsCritical()
        {
            return new Random().Next(1, 101) <= 10;
        }

        private int CalculateDamage(Monstro target, bool isCritical)
        {
            int baseDamage = Nivel * 5;

            if (isCritical)
                baseDamage *= 2;

            if (target.Weakness == Elemento)
                baseDamage *= 2;

            return baseDamage;
        }
        //Final metodo ataque e critico


        
        //Inicio Mostrar Barras de vida e de Mana
        public string BarraVida()
        {
            return $"Vida: {Vida} / {VidaMaxima}";
        }

        public string BarraMana()
        {
            return $"Mana: {Mana} / {ManaMaxima}";
        }
        //Final Mostrar Barras de vida e Mana
    }
}
