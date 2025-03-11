import React, { useRef, useState, useEffect } from "react";
import { View, Text, ImageBackground, StyleSheet, Dimensions, ScrollView } from "react-native";
import CustomButton from "../components/CustomButton";

const { width, height } = Dimensions.get("window");

const slides = [
  {
    id: "1",
    title: "Benefícios Exclusivo!",
    description: "Nosso programa foi criado para oferecer mais conveniência para clinicas e médicos.",
  },
  {
    id: "2",
    title: "Vantagens do Programa",
    description: "✅ Aumento das consultas\n✅ Muitas recompensas\n✅ Satisfação\n✅ Aumente sua carteira de clientes sem esforço.",
  },
  {
    id: "3",
    title: "Como funciona?",
    description: "1️⃣ Complete seu cadastro\n2️⃣ Aceite as sugestões de consultas \n3️⃣ Você recebe uma notificação de confirmação \n4️⃣ Não perca as consultas\n5️⃣ Crie alguns vídeos preventivos \n6️⃣ Aproveite as vantagens e aumente sua carteira com nosso programa.",
  },
];

const SobreProgramaBeneficiosScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  const scrollViewRef = useRef<ScrollView>(null);
  const [activeIndex, setActiveIndex] = useState(0);

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
    <ImageBackground source={require("../assets/ProgramaBeneficios/background-recortada.jpg")} style={styles.background}>
      <View style={styles.container}>

        {/* Cards mais para baixo */}
        <ScrollView
          ref={scrollViewRef}
          horizontal
          pagingEnabled
          showsHorizontalScrollIndicator={false}
          onScroll={handleScroll}
          scrollEventThrottle={16}
          style={styles.scrollView}
        >
          {slides.map((item, index) => (
            <View key={index} style={styles.card}>
              <Text style={styles.title}>{item.title}</Text>
              <Text style={styles.description}>{item.description}</Text>
            </View>
          ))}
        </ScrollView>

        {/* Indicadores de progresso */}
        <View style={styles.indicatorContainer}>
          {slides.map((_, index) => (
            <View key={index} style={[styles.indicator, activeIndex === index && styles.activeIndicator]} />
          ))}
        </View>

      </View>

      <View style={styles.buttonContainer}>
        <CustomButton title="Conheça as Recompensas" onPress={() => navigation.navigate("ProgramaBeneficioClinica")} width={"80%"} textColor="#fff" />
      </View>

    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  background: {
    flex: 1,
    resizeMode: "cover",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
  },
  container: {
    flex: 1,
    justifyContent: "flex-end",
    alignItems: "center",
    paddingBottom: 40,
  },
  scrollView: {
    flexGrow: 0,
    marginBottom: 100,
  },
  card: {
    width: width * 0.9,
    backgroundColor: "rgba(255, 255, 255, 0.7)",
    padding: 10,
    borderRadius: 12,
    alignItems: "center",
    marginHorizontal: 20,
    height: height * 0.3,
    justifyContent: "center",
    alignContent:'flex-start',
  },
  title: {
    fontSize: 20,
    fontWeight: "semibold",
    color: "#0A4275",
    textAlign: "left",
    marginBottom: 10,
  },
  description: {
    fontSize: 16,
    color: "#081828",
    textAlign: "left",
    lineHeight: 22,
  },
  indicatorContainer: {
    flexDirection: "row",
    position: "absolute",
    bottom: 60,
  },
  indicator: {
    width: 10,
    height: 10,
    borderRadius: 5,
    backgroundColor: "#555",
    marginHorizontal: 5,
  },
  activeIndicator: {
    backgroundColor: "#08c8f8",
    width: 14,
    height: 14,
    borderRadius: 7,
  },
  buttonContainer: {
    width: "90%",
    alignItems: "center",
    marginBottom: 20,
  },
});

export default SobreProgramaBeneficiosScreen;
