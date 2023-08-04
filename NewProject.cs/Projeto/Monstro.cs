using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NewProject.cs.Projeto
{
    enum TipoMonstro
    {
        Zumbi,
        Esqueleto,
        Lobo,
        Aranha,
        Gigante,
        Dragao
    }
    internal class Monstro
    {
        public string Nome { get; set; }
        public int Nivel { get; set; }
        public int Forca { get;set; }
        public int Defesa { get; set; }
        public int Agilidade { get; set; }
        public int Vida { get;  set; }
        public int VidaMaxima { get; set; }
        public int VidaMaximaInicial { get; set; }
        public TipoMonstro TipoMonstro { get; set; }    
        public Dictionary<int, string> Attacks { get; set; }
        public TiposDeAfinidades Element { get; set; }
        public TiposDeAfinidades Weakness { get; set; }

        private static readonly Random random = new Random();

        public Monstro(string nome, int nivel, TipoMonstro tipoMonstro, TiposDeAfinidades element, TiposDeAfinidades weakness)
            
        {
            Nome = nome;
            Nivel = nivel;
            Element = element;
            Weakness = weakness;

            switch (tipoMonstro)
            {
                case TipoMonstro.Zumbi:
                    Forca = 50 + nivel * 2;
                    Defesa = 10 + nivel * 2;
                    Agilidade = 30 + nivel * 2;
                    VidaMaxima = 100 + nivel * 20;
                    break;
                case TipoMonstro.Gigante:
                    Forca = 80 + nivel * 2;
                    Defesa = 30 + nivel * 2;
                    Agilidade = 50 + nivel * 2;
                    VidaMaxima = 200 + nivel * 30;
                    break;
                case TipoMonstro.Esqueleto:
                    Forca = 80 + nivel * 2;
                    Defesa = 20 + nivel * 2;
                    Agilidade = 80 + nivel * 2;
                    VidaMaxima = 100 + nivel * 20;
                    break;
                case TipoMonstro.Dragao:
                    Forca = 120 + nivel * 2;
                    Defesa = 50 + nivel * 2;
                    Agilidade = 100 + nivel * 2;
                    VidaMaxima = 300 + nivel * 35;
                    break;
                case TipoMonstro.Lobo:
                    Forca = 70 + nivel * 2;
                    Defesa = 15 + nivel * 2;
                    Agilidade = 150 + nivel * 2;
                    VidaMaxima = 100 + nivel * 25;
                    break;
                case TipoMonstro.Aranha:
                    Forca = 50 + nivel * 2;
                    Defesa = 15 + nivel * 2;
                    Agilidade = 130 + nivel * 2;
                    VidaMaxima = 100 + nivel * 25;
                    break;
            }
            VidaMaxima = 100 + nivel * 20;
            Vida = VidaMaxima;

        }
        public Monstro Clone()
        {
            return new Monstro(Nome, Nivel, TipoMonstro, Element, Weakness)
            {
                Forca = this.Forca,
                Defesa = this.Defesa,
                Agilidade = this.Agilidade,
                Vida = this.VidaMaximaInicial // Ao clonar, definimos a vida como a vida máxima inicial
            };
        }

        //Codigo para atacar o personagem
        public void Atacar(Personagens personagens)
        {
            int dano = Forca + random.Next(-2, 3);
            int danoCausado = Math.Max(dano - personagens.Defesa, 0);
            personagens.Vida -= danoCausado;
           // personagens.SofreDano(danoCausado);
            Console.WriteLine($"{Nome} causou {danoCausado} de dano a você!");
        }

        public void AtaqueEspecial(Personagens personagens)
        {
            int dano = Forca + random.Next(1, 15);
            int danoCausado = Math.Max(dano - personagens.Defesa, 0);
            personagens.Vida -= danoCausado;
            //personagens.SofreDano(danoCausado);
            Console.WriteLine($"{Nome} causou {danoCausado} de dano especial a você!");
        }
    }
    

}
