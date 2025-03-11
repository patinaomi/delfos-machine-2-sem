import React, { useRef, useState, useEffect } from "react";
import { View, Text, ImageBackground, StyleSheet, Dimensions, ScrollView } from "react-native";
import CustomButton from "../components/CustomButton";

const { width, height } = Dimensions.get("window");

const slides = [
  {
    id: "1",
    title: "O que te espera?",
    description: "Conheça um novo jeito de cuidar da sua saúde bucal! Nosso sistema inteligente antecipa suas necessidades e sugere consultas preventivas, garantindo mais saúde e menos custos. ",
    image: require("../assets/onboarding/primeira.png"),
  },
  {
    id: "2",
    title: "Agendamentos Inteligentes",
    description: "Esqueça a preocupação com marcações! Nosso sistema sugere automaticamente consultas no dia, horário e local de sua preferência, garantindo que seu sorriso se mantenha saudável a cada cinco ou seis meses e evitando gastos inesperados com tratamentos de última hora. Preencha apenas algumas informações e nossa inteligência, que atua por trás dos bastidores, proporcionará uma experiência diferenciada.",
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
    title: "Gestão Simplificada",
    description: "Gerencie suas consultas e sugestões de consultas com poucos cliques.",
    image: require("../assets/onboarding/quinta.jpg"),
  },

  {
    id: "5",
    title: "Programa de Benefícios Exclusivo",
    description: "Executando algumas atividades simples, você pontua e ganha recompensas incriveis.",
    image: require("../assets/onboarding/setima.png"),
  },
  {
    id: "6",
    title: "Comece agora!",
    description: "Faça seu cadastro, em seguida o login e aproveite os benefícios! O futuro da odontologia preventiva começa agora, e você faz parte dessa transformação!",
    image: require("../assets/onboarding/decima.png"),
  },
];


const OnboardingClientesScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
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
        <CustomButton title="Pular" textColor="#fff" onPress={() => navigation.replace("Login")} width="90%" />
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

export default OnboardingClientesScreen;
