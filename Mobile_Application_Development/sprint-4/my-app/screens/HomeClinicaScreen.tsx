import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { View, Text, Button, TouchableOpacity, ImageBackground } from 'react-native';
import CustomButton from '../components/CustomButton';
import Footer from '../components/Footer';
import { StyleSheet } from "react-native";

const HomeClinicaScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  return (
    <ImageBackground source={require("../assets/background/tela-inicial-clinicas.png")} style={styles.background}>
      <View style={styles.container}>
        {/* Título */}
        <Text style={styles.title}>Transforme a experiência dos seus pacientes com agendamentos inteligentes e gestão simplificada. Seja uma clínica parceira da Delfos Machine e leve inovação ao seu atendimento!</Text>

        <CustomButton title="Cadastro" onPress={() => navigation.navigate("CadastroClinica")} width={'100%'} textColor='#fff'/>

        <CustomButton title="Login" onPress={() => navigation.navigate("LoginClinica")} width={'100%'} textColor='#fff'/>

      </View>

      {/* Rodapé */}
      <Footer textColor='#0A4275'/>

    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    //backgroundColor: "#081828",
    justifyContent: "center",
    alignItems: "flex-start",
    width:'100%',
    paddingHorizontal: 20,
  },
  title: {
    fontSize: 22,
    fontWeight: "bold",
    color: "#0A4275",
    textAlign: "left",
    marginBottom: 40,
    maxWidth: '60%'
  },
  button: {
    backgroundColor: "#08c8f8",
    paddingVertical: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 20,
  },
  buttonText: {
    fontSize: 16,
    fontWeight: "bold",
    color: "#fff",
  },
  background: {
    flex: 1,
    resizeMode: "cover",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
    opacity: 20,
  },
});
export default HomeClinicaScreen;
