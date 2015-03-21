/*
    personality modeling framework
    Copyright (C) 2007 Bob Mottram
    fuzzgun@gmail.com

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Text;
using sluggish.utilities.xml;
using cuteagi.core;

namespace cuteagi.personalitymodel
{
    public class belief : nodeTemporal
    {
        #region "belief systems"

        public static String[] BeliefSystemType = {
            "ableism", "abolitionism", "absenteeism", "absolutism", "abstract expressionism", "absurdism", "accidentalism", "acosmism", "activism", "adoptionism", "adultism", "aestheticism",
            "Afrocentrism", "ageism", "agnosticism", "agorism", "agrarianism", "alarmism", "Albigensianism", "albinism", "alcoholism", "alter-globalization", "Althusserianism", "altruism", "Americanism", "American exceptionalism",
            "anachronism", "anarchism", "anarcho-capitalism", "anarcho-communism", "anarcho-syndicalism", "Anglicanism", "Anglicism", "aniconism", "animalism", "animism", "anthropocentrism", "anthropomorphism",
            "anti-Americanism", "anti-abolitionism", "anticlericalism", "antidisestablishmentarianism", "anti-imperialism", "anti-intellectualism", "antinomianism", "anti-Polonism", "anti-Semitism", "anti-Turkism", 
            "anti-Zionism", "Apollinarism", "archaism", "Arianism", "Aristotelianism", "atavism", "atheism", "Atlanticism", "atomism", "Atticism", "Audism", "Augustinism", "Australianism", "authoritarianism", "autism", "Averroism",
            "Ba'athism", "baalism", "Babaism", "Bábism", "Bagism", "Bahá'ísm", "bagism", "baptism", "beardism", "behaviourism", "Belgicism", "Benthamism", "bicameralism", "bilateralism", "bilingualism", "bimetallism", "bjjism", "bipedalism", "Blairism", "bogyism", "bohemianism", "Bolshevism", "Bonapartism", "bonism", "boosterism", "bossism", "botulism", "breatharianism", "brianism", "bromism", "brutalism", "brutism", "bruxism", "Buchmanism", "Buddhism", "Bukharinism", "Bullism", "Bushism", "Buzaism",
            "Caesaropapism", "Calvinism", "cambism", "cannibalism", "Caodaism", "capitalism", "careerism", "Carlism", "Cartesian dualism", "Cartesianism", "Castroism", "Catharism", "Catholicism", "centralism", "centrism", "charlatanism", "Chartism", "Chassidism", "chauvinism", "checkbook journalism", "chemism", "classicism", "classism", "clintonism", "clericalism", "Clitorism", "clonism", "cognitivism", "coherentism", "collectivism", "collectivist-anarchism", "colloquialism", "colonialism", "commercialism", "communalism", "communism", "compassionate conservatism", "Comtism", "conformism", "conformitarianism", "Confucianism", "consequentialism", "conservatism", "Conservative Judaism", "contextualism", "contrarianism", "constructivism", "consubstantiationism", "consumerism", "corealism", "corporatism", "cosmism", "cosmopolitanism", "cosmotheism", "creationism", "criticism", "cronyism", "cubism", "cultism", "cultural relativism", "cynicism", "czarism",
            "Dadaism", "Daoism", "Darwinism", "deconstructivism", "decontextualism", "defeatism", "deism", "deontologism", "decentralism", "despotism", "determinism", "deviatonism", "disablism", "Discordianism", "disestablishmentarianism", "Docetism", "dodoism", "dogmatism", "Dominionism", "Donatism", "do-goodism", "do-nothingism", "druidism", "dualism", "dvanteism", "dwarfism", "dynamism",
            "echoism", "Edwardianism", "effeminism", "egalitarianism", "egocentrism", "egoism", "electromagnetism", "eliminativism", "elitism", "embolism", "empiricism", "environmental racism", "environmentalism", "erotism", "escapism", "essentialism", "etatism", "eternalism", "ethicism", "ethnic nationalism", "ethnocentrism", "Eudemonism", "Eurocentrism", "Eurocommunism", "evolutionism", "existentialism", "exotism", "expansionism", "expressionism", "externalism", "externism", "extremism", "extrinsicism", "extropism",
            "fabianism", "faddism", "Falangism", "fanaticism", "fascism", "fatalism", "Fauvism", "fecundism", "federalism", "Feeneyism", "feminism", "Fenianism", "fetishism", "feudalism", "fideism", "finitism", "fogeyism", "Food faddism", "foot-fetishism", "Fordism", "formalism", "foundationalism", "Francoism", "freeganism", "functionalism (philosophy of mind)", "functionalism (sociology)", "fundamentalism", "futurism",
            "Gallicism", "Gaullism", "gay liberationism", "geocentrism", "Geoism", "Georgianism", "Georgism", "Germanism", "gigantism", "globalism", "Gnosticism", "Goreism", "Godism", "gradualism", "Grundyism",
            "Hacktivism", "Haruhiism", "Hassidism", "healthism", "hedonism", "Hegelianism", "heightism", "Hellenism", "heliocentrism", "Hellenism", "henotheism", "heroism", "heterosexism", "heterosexualism", "Hinduism", "hirsutism", "historicism", "Hitlerism", "Hobbesianism", "hoboism", "homoousianism", "Horthyism", "hucksterism", "humanism", "Hussitism", "hypnotism",
            "idealism", "idolism", "imagism", "immanentism", "imperialism", "impressionism", "individualism", "independentism", "ineffablilism", "institutionalism", "instructivism", "intellectualism", "internationalism (linguistics)", "internationalism (politics)", "interventionism", "intrinsicism", "irenicism", "irredentism", "Islamism", "isolationism",
            "Jacobinism", "Jacobitism", "Jainism", "Jansenism", "Japonism", "jingoism", "journalism", "Judaism", "jujuism", "juridical positivism",
            "Kantianism", "kathenotheism", "Keynesianism", "Kemalism", "Kimilsungism",
            "laborism", "laicism", "lamaism", "Lamarckism", "Lancastrianism", "leftism", "left-liberalism", "legalism", "legal positivism", "left-anarchism", "Leninism", "lesbianism", "liberalism", "libertarianism", "Linuxism", "localism", "locoism", "logical positivism", "Lollardism", "luminism", "Lutheranism", "Luxemburgism", "lycanthropism",
            "mpism", "magnetism", "malapropism", "Manichaeanism", "Manualism", "Maoism", "Marxism", "Marxism-Leninism", "masculism", "Masochism", "materialism", "maximalism", "McCarthyism", "mechanism", "Medism", "melanism", "Menshevism", "Mercantilism", "mesmerism", "metabolism", "Methodism", "militarism", "millennialism", "minarchism", "minimalism", "Mithraism", "modalism", "Modernism", "Mohammedanism", "Mohism", "Monarchianism", "monarchism", "monetarism", "Mongolism", "monism", "monolatrism", "Monophysitism", "monotheism", "monotropism", "moral absolutism", "moral naturalism", "moralism", "moral positivism", "moral relativism", "Mormonism", "multiculturalism", "multilateralism", "musicism", "mysticism",
            "narcissism", "nationalism", "National Socialism", "nativism", "naturalism", "naturism", "Nazism", "negativism", "neo-classicism", "neoconservatism", "neo-Darwinism", "neoism", "neolibertarianism", "neopaganism", "neoplatonism", "neoclassicism", "neoclassicism (music)", "neo-romanticism", "neoromanticism (music)", "nepotism", "Nestorianism", "nihilism", "nomianism", "non-consequentialism", "non-interventionism", "nonsituationism", "nudism",
            "obelism", "objectivism", "occultism", "ogreism", "oligarchism", "olympism", "onanism", "ontologism", "optimism", "oralism", "Orangism", "orientalism",
            "Pabloism", "pacifism", "paganism", "paleoconservatism", "paleolibertarianism", "pan-Africanism", "pan-Arabism", "pandeism", "panentheism", "pantheism", "pan-Germanism", "pan-Slavism", "pantheism", "pan-Turkism", "papism", "parliamentism", "parliamentarism", "patriotism", "Patripassianism", "Pelagianism", "Pentecostalism", "peonism", "perennialism", "Peronism", "Persianism", "personalism", "pessimism", "phallocentrism", "pharaonism", "phenomenalism", "philhellenism", "philistinism", "panism", "pietism", "Pilgerism", "plagiarism", "playganism", "platonism", "plenism", "pointillism", "politicalism", "political absolutism", "political partyism", "polycentrism", "polydeism", "polymorphism", "polytheism", "populism", "positivism", "postcolonialism", "post-Freudianism", "postimpressionism", "post-Keynesianism", "postmodernism", "poststructuralism", "post-Zionism", "pragmatism", "Presbyterianism", "presenteeism", "presentism", "priapism", "primitivism", "progressivism", "protectionism", "Protestantism", "proto-capitalism", "Psilanthropism", "Puseyism",
            "racism", "radicalism", "Raelism", "raptivism", "rationalism", "reactionarism", "Reaganism", "realism", "recidivism", "redneckism", "reductionism", "regionalism", "relativism", "reliabilism", "republicanism", "Republicanism", "revanchism", "revisionism", "rheumatism", "rightism", "Roman Catholicism", "romanticism", "Rosicrucianism", "Rousseauism", "rugged individualism",
            "Sabbatarianism", "Sabellianism", "sadism", "sado-masochism", "Saint-simonism", "Salvationism", "Sandinism", "sanism", "Sapphism", "Satanism", "Scandinavism", "scepticism", "Schmederalism", "scholasticism", "Scientism", "Scotticism", "sectarianism", "sectionalism", "secularism", "sensualism", "separatism", "sesquipedalianism", "sexism", "Shachtmanism", "shamanism", "Shiism", "Shintoism", "Sikhism", "Situationism", "sizism", "skepticism", "slumism", "social Darwinism", "social realism", "socialism", "socialist realism", "Socianism", "solecism", "solipsism", "sophism", "sovereigntism", "Sparticism", "speciesism", "Spenglerism", "spiritism", "spiritualism", "Spoonerism", "Stalinism", "standpatism", "stasism", "statism", "stoicism", "structuralism", "Stuckism", "Sufism", "surrealism", "syllogism", "symbolism", "syndicalism", "syncretism",
            "tachism", "Tanzerizm", "Taoism", "Tarantism", "Taylorism", "terrorism", "Thatcherism", "theism", "Thomism", "Titoism", "Toryism", "tourism", "totalitarianism", "transcendentalism", "transgenderism", "transhumanism", "transsexualism", "transsubstantiationism", "transvestism", "tribalism", "tricameralism", "Trinitarianism", "tropism", "Trotskyism", "tsarism", "Turanism",
            "Ultraintuitionism", "Ultraleftism", "Ultramontanism", "Unificationism", "unicameralism", "Uniformitarianism", "unilateralism", "Unionism", "Unitarianism", "unitarism", "Universalism", "uranism", "utilitarianism", "utopianism",
            "vampirism", "vandalism", "veganism", "vegetarianism", "Victorianism", "Vorticism", "voyeurism", "vulgarism",
            "Wahhabism", "Whiggism", "Whitlamism", "witticism",
            "xenocentrism",
            "yellow journalism", "Yezidism",
            "Zapatism", "Zionism", "Zoroastrianism", "Zwingliism"
        };

        #endregion

        public truthvalue Strength;
        public String BeliefSystem;
        public int RelinquishedAge;
        
        #region "constructors"
        
        public belief()
        {
            SetNodeType("belief");
            Strength = new truthvalue();
        }
        
        #endregion

        #region "saving and loading"

        public override XmlElement getXML(XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement("Belief");
            doc.DocumentElement.AppendChild(elem);
            xml.AddTextElement(doc, elem, "Strength", Strength.ToString());
            xml.AddTextElement(doc, elem, "BeliefSystem", BeliefSystem);
            xml.AddTextElement(doc, elem, "AcquiredDate", GetAcquiredDate().ToString());
            xml.AddTextElement(doc, elem, "RelinquishedAge", Convert.ToString(RelinquishedAge));
            return (elem);
        }
        
        /// <summary>
        /// parse an xml node
        /// </summary>
        /// <param name="xnod"></param>
        /// <param name="level"></param>
        public override void LoadFromXml(XmlNode xnod, int level)
        {
            LoadNodeFromXml(xnod, level);
            
            if (xnod.Name == "Strength")
                Strength = truthvalue.FromString(xnod.InnerText);
                
            if (xnod.Name == "BeliefSystem")
                BeliefSystem = xnod.InnerText;
                
            if (xnod.Name == "AcquiredDate")
                SetAcquiredDate(DateTime.Parse(xnod.InnerText));
                
            if (xnod.Name == "RelinquishedAge")
                RelinquishedAge = Convert.ToInt32(xnod.InnerText);

            // call recursively on all children of the current node
            if (xnod.HasChildNodes)
            {
                XmlNode xnodWorking = xnod.FirstChild;
                while (xnodWorking != null)
                {
                    LoadFromXml(xnodWorking, level + 1);
                    xnodWorking = xnodWorking.NextSibling;
                }
            }
        }        

        #endregion

    }
}


