import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { View, Text, TouchableOpacity, ImageBackground } from 'react-native';
import CustomButton from '../components/CustomButton';
import Footer from '../components/Footer';
import { StyleSheet } from "react-native";

const HomeClienteScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  return (
    <ImageBackground source={require("../assets/HomeClientes/background-dois.jpg")} style={styles.background}>
      <View style={styles.container}>
        {/* Card com texto */}
        <View style={styles.card}>
          <Text style={styles.title}>
            Imagine receber uma notificação informando que sua consulta foi agendada de acordo com a sua disponibilidade, no local mais conveniente, sem precisar perder tempo procurando ou se preocupando com isso.
          </Text>
          <Text style={styles.title}>
            Com nosso sistema inteligente, você tem mais comodidade e tranquilidade, sabendo que seu agendamento está sendo feito da melhor forma para você.
          </Text>
        </View>

        {/* Botões */}
        <View style={styles.buttonsContainer}>
          <CustomButton title="cadastro" onPress={() => navigation.navigate("Cadastro")} width={'100%'} textColor='#fff'/>
          <CustomButton title="login" onPress={() => navigation.navigate("LoginCliente")} width={'100%'} textColor='#fff'/>
        </View>
      </View>

      {/* Rodapé */}
      <Footer textColor='#0A4275'/>
    </ImageBackground>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    width: '100%',
    paddingHorizontal: 20,
  },
  card: {
    backgroundColor: "rgba(255, 255, 255, 0.7)",
    padding: 20,
    borderRadius: 10,
    maxWidth: '100%', 
    marginBottom: 30,
    alignItems: "center",
  },
  title: {
    fontSize: 18,
    fontWeight: "semibold",
    color: "#0A4275", 
    textAlign: "left",
    marginBottom: 20,
  },
  buttonsContainer: {
    width: '100%',
    marginTop: 20,
  },
  background: {
    flex: 1,
    resizeMode: "cover",
    justifyContent: "center",
    alignItems: "center",
    width: "100%",
    height: "100%",
  },
});

export default HomeClienteScreen;
