import React, { useRef, useState, useEffect } from "react";
import { View, Text, ImageBackground, StyleSheet, Dimensions, ScrollView } from "react-native";
import CustomButton from "../components/CustomButton";

const { width, height } = Dimensions.get("window");

const slides = [
  {
    id: "1",
    title: "Agendamentos Inteligentes",
    description: "Facilite a vida dos seus clientes com um sistema de agendamento ágil e inteligente. Evite faltas e ofereça sugestões automáticas de horários conforme sua disponibilidade, garantindo um atendimento mais organizado e eficiente.",
    image: require("../assets/onboardingFormatado/first.jpg"),
  },
  {
    id: "2",
    title: "Gestão Simples e Eficiente",
    description: "Gerencie horários e pacientes de forma simples e eficiente, tudo em um só lugar. O acesso é feito apenas aos horários disponíveis dos médicos e às atividades da clínica, garantindo praticidade e organização com apenas um clique.",
    image: require("../assets/onboarding/setima.png"),
  },
  {
    id: "3",
    title: "Soluções para clínicas e pacientes",
    description: "Seja parceiro da Delfos Machine e leve inovação para sua clínica.",
    image: require("../assets/onboarding/quarto.png"),
  },
  {
    id: "4",
    title: "Nossa Solução",
    description: "Com apenas um clique, gerencie seus serviços de forma prática e eficiente! Aceite ou recuse agendamentos com base na sua disponibilidade, sem complicações. Assim que um horário compatível for encontrado, seu cliente receberá uma notificação e, com um simples toque, poderá confirmar a consulta. Mais facilidade para você, mais conveniência para seus pacientes!",
    image: require("../assets/onboarding/quinta.png"),
  },
  {
    id: "5",
    title: "Consulta e Feedbacks",
    description: "Agendamento fácil, atendimento ágil e feedback valioso! Após a consulta, seus clientes podem avaliar o serviço, ajudando a aprimorar ainda mais a experiência. Simples, rápido e eficiente!",
    image: require("../assets/onboarding/sexta.png"),
  },
  {
    id: "6",
    title: "Programa de Benefícios",
    description: "Atenda mais, fidelize seus clientes e ganhe recompensas exclusivas! Com nosso programa de benefícios, cada consulta concluída e bem avaliada gera pontos para sua clínica. Quanto mais agendamentos, maiores as vantagens! Participe e transforme seu atendimento em um diferencial competitivo!",
    image: require("../assets/onboarding/oitava.png"),
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
    alignItems: "center",
  },
  title: {
    fontSize: 18,
    fontWeight: "bold",
    color: "#000",
    textAlign: "center",
  },
  description: {
    fontSize: 18,
    color: "#000",
    textAlign: "center",
    marginTop: 30,
    paddingHorizontal: 10,
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
