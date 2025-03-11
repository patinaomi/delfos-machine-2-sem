import React, { useRef, useState, useEffect } from "react";
import { View, Text, ImageBackground, StyleSheet, Dimensions, ScrollView } from "react-native";
import CustomButton from "../components/CustomButton";

const { width, height } = Dimensions.get("window");

const slides = [
  {
    id: "1",
    title: "A Revolução na Odontologia",
    description: "Conheça um novo jeito de cuidar da sua saúde bucal! Nosso sistema inteligente antecipa suas necessidades e sugere consultas preventivas, garantindo mais saúde e menos custos.",
    image: require("../assets/onboarding/primeira.png"),
  },
  {
    id: "2",
    title: "Agendamentos Inteligentes",
    description: "Esqueça a preocupação com marcações! Nosso sistema sugere automaticamente consultas para manter seu sorriso saudável, evitando gastos inesperados com tratamentos de última hora.",
    image: require("../assets/onboarding/segunda.png"),
  },
  {
    id: "3",
    title: "Prevenção para economizar",
    description: "Manter consultas regulares evita problemas graves e reduz custos para você e para o seu plano odontológico. Nossa tecnologia ajuda você a cuidar da sua saúde bucal sem surpresas!",
    image: require("../assets/onboarding/quinta.png"),
  },
  {
    id: "4",
    title: "Mais pacientes para sua Clínica",
    description: "Clínicas parceiras recebem pacientes automaticamente! Nossa plataforma gerencia a agenda e preenche horários ociosos, garantindo fluxo contínuo de atendimentos.",
    image: require("../assets/onboarding/quarto.png"),
  },
  {
    id: "5",
    title: "Gestão Simplificada",
    description: "Gerencie horários e pacientes de forma prática e eficiente! Com apenas um clique, sua clínica aprova ou ajusta consultas sugeridas, garantindo um atendimento mais organizado.",
    image: require("../assets/onboarding/quinta.jpg"),
  },
  {
    id: "6",
    title: "Redução de custos para Seguradoras",
    description: "Com mais prevenção, menos urgências! Nosso sistema ajuda operadoras de planos odontológicos a reduzir custos, garantindo maior eficiência e sustentabilidade.",
    image: require("../assets/onboarding/sexta.png"),
  },
  {
    id: "7",
    title: "Programa de Benefícios Exclusivo",
    description: "Clientes e clínicas ganham recompensas ao participarem ativamente! Consultas realizadas acumulam pontos que podem ser trocados por vantagens e descontos.",
    image: require("../assets/onboarding/setima.png"),
  },
  {
    id: "8",
    title: "Tecnologia a serviço da Saúde",
    description: "Utilizamos inteligência artificial para oferecer um serviço personalizado e eficiente. Você recebe lembretes, acompanha seu histórico e mantém sua saúde bucal sempre em dia!",
    image: require("../assets/onboarding/oitava.png"),
  },
  {
    id: "9",
    title: "Parceria com Clínicas e Seguradoras",
    description: "Junte-se à nossa rede de clínicas e seguradoras para oferecer a melhor experiência odontológica aos seus clientes, aumentando a fidelização e reduzindo custos operacionais.",
    image: require("../assets/onboarding/nona.png"),
  },
  {
    id: "10",
    title: "Comece agora!",
    description: "Baixe o app, faça seu cadastro e aproveite os benefícios! O futuro da odontologia preventiva começa agora, e você faz parte dessa transformação!",
    image: require("../assets/onboarding/decima.png"),
  },
];


const OnboardingScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const scrollViewRef = useRef<ScrollView>(null);
  const [activeIndex, setActiveIndex] = useState(0);

  // Função para alternar os slides automaticamente
  useEffect(() => {
    const interval = setInterval(() => {
      scrollToNextSlide();
    }, 10000);

    return () => clearInterval(interval); 
  }, []);

  const scrollToNextSlide = () => {
    const nextIndex = (activeIndex + 1) % slides.length;
    setActiveIndex(nextIndex);
    scrollViewRef.current?.scrollTo({ x: nextIndex * width, animated: true });
  };

  const handleScroll = (event: any) => {
    const slideIndex = Math.round(event.nativeEvent.contentOffset.x / width);
    setActiveIndex(slideIndex);
  };

  return (
    <View style={styles.container}>
      <ScrollView
        ref={scrollViewRef}
        horizontal
        pagingEnabled
        showsHorizontalScrollIndicator={false}
        onScroll={handleScroll}
        scrollEventThrottle={16}
      >
        {slides.map((item, index) => (
          <ImageBackground key={index} source={item.image} style={styles.slide}>
            <View style={styles.textContainer}>
              <Text style={styles.title}>{item.title}</Text>
              <Text style={styles.description}>{item.description}</Text>
            </View>
          </ImageBackground>
        ))}
      </ScrollView>

      {/* Indicadores de progresso, vai ajudar a saber quantas fotos existem */}
      <View style={styles.indicatorContainer}>
        {slides.map((_, index) => (
          <View key={index} style={[styles.indicator, activeIndex === index && styles.activeIndicator]} />
        ))}
      </View>

      {/* Botão de Pular */}
      <View style={styles.buttonContainer}>
        <CustomButton title="Pular" textColor="#fff" onPress={() => navigation.replace("Home")} width="90%" />
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#081828",
    justifyContent: "center",
    alignItems: "center",
    paddingBottom: 40,
  },
  slide: {
    width: width,
    height: height,
    justifyContent: "flex-end",
    alignItems: "center",
  },
  textContainer: {
    width: "90%",
    backgroundColor: "rgba(255, 255, 255, 0.8)",
    padding: 20,
    borderRadius: 12,
    marginBottom: 200,
    alignItems: "flex-start",
  },
  title: {
    fontSize: 22,
    fontWeight: "semibold",
    color: "#000",
    textAlign: "left",
    paddingHorizontal: 0,
  },
  description: {
    fontSize: 18,
    color: "#000",
    textAlign: "left",
    marginTop: 10,
    paddingHorizontal: 0,
    lineHeight: 24,
    letterSpacing: -0.3,
  },
  indicatorContainer: {
    flexDirection: "row",
    position: "absolute",
    bottom: 80,
  },
  indicator: {
    width: 10,
    height: 10,
    borderRadius: 5,
    backgroundColor: "#555",
    marginHorizontal: 5,
    marginBottom: 100,
  },
  activeIndicator: {
    backgroundColor: "#08c8f8",
    width: 14,
    height: 14,
    borderRadius: 7,
  },
  buttonContainer: {
    position: "absolute",
    bottom: 40,
    width: "100%",
    alignItems: "center",
  },
});

export default OnboardingScreen;
