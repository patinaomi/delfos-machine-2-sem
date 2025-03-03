import React, { useEffect, useState } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { View, Text, Button, TouchableOpacity, ImageBackground } from 'react-native';
import CadastroScreen from './screens/CadastroScreen';
import SucessoScreen from './screens/SucessoScreen';
import LoginScreen from './screens/LoginScreen';
import SessaoRestritaScreen from './screens/SessaoRestritaScreen';
import DadoPessoalScreen from './screens/DadoPessoalScreen';
import ConsultarDadosScreen from './screens/ConsultarDadosScreen';
import { StyleSheet } from "react-native";
import CustomButton from "./components/CustomButton";
import Footer from './components/Footer';
import HomeClinicaScreen from './screens/HomeClinicaScreen';
import CadastroClinicaScreen from './screens/CadastroClinicaScreen';
import LoginClinicaScreen from './screens/LoginClinicaScreen';
import SessaoRestritaClinicaScreen from './screens/SessaoRestritaClinicaScreen';
import OnboardingScreen from './screens/OnboardingScreen';
import { AuthProvider } from "./contexts/AuthProvider";
import { auth } from './src/firebaseConfig';
import DadosClinicaScreen from './screens/DadosClinicaScreen';
import ConsultarDadosClinicaScreen from './screens/ConsultarDadosClinicaScreen';
import CadastrarMedicosScreen from './screens/CadastrarMedicosScreen';
import ConsultarDadosMedicosScreen from './screens/ConsultarDadosMedicosScreen';
import SugestaoServicosClinicaScreen from './screens/ServicosClinicaScreen';

const Stack = createStackNavigator();

const HomeScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  return (
    <ImageBackground source={require("../my-app/assets/background/tela-inicial-opcoes.png")} style={styles.background}>
        <View style={styles.container}>
          {/* Título */}
          <Text style={styles.title}>Bem-vindo ao seu Agendamento Inteligente</Text>

            <Text style={styles.titleSecond}>Para Clientes</Text>
            <CustomButton title="Cadastro" textColor='#fff' onPress={() => navigation.navigate("Cadastro")} width={'100%'}/>
          
            <CustomButton title="Login" textColor='#fff' onPress={() => navigation.navigate("Login")} width={'100%'}/>
    

          <Text style={styles.titleSecond}>Para Clínicas</Text>

          <CustomButton title="Parceiros" textColor='#fff' onPress={() => navigation.navigate("HomeClinica")} width={'100%'}/>

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
    alignItems: "center",
    paddingHorizontal: 10,
    width: '100%'
  },
  title: {
    fontSize: 32,
    fontWeight: "bold",
    color: "#0A4275",
    textAlign: "left",
    marginBottom: 50,
    width:'100%'
  },
  titleSecond: {
    fontSize: 22,
    fontWeight: "bold",
    color: "#063970",
    textAlign: "left",
    marginBottom: 30,
    width:'100%'
  },
  button: {
    backgroundColor: "#08c8f8",
    paddingVertical: 12,
    borderRadius: 8,
    alignItems: "center",
    marginBottom: 40,
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
export default function App() {

  const [user, setUser] = useState<any>(null);

  useEffect(() => {
    const unsubscribe = auth.onAuthStateChanged((user) => {
      setUser(user);
    });

    return () => unsubscribe();
  }, []);
  
  return (
    <AuthProvider>
      <NavigationContainer>
        <Stack.Navigator>
          <Stack.Screen name="Onboarding" component={OnboardingScreen} options={{ headerShown: false }} />
          <Stack.Screen name="Home" component={HomeScreen} />
          <Stack.Screen name="Cadastro" component={CadastroScreen} />
          <Stack.Screen name="Login" component={LoginScreen} />
          <Stack.Screen name="Sucesso" component={SucessoScreen} />
          <Stack.Screen name="SessaoRestrita" 
          component={SessaoRestritaScreen} />
          <Stack.Screen name="DadoPessoal" component={DadoPessoalScreen} />
          <Stack.Screen name="ConsultarDados" component={ConsultarDadosScreen} />
          <Stack.Screen name="HomeClinica" component={HomeClinicaScreen} />
          <Stack.Screen name="CadastroClinica" component={CadastroClinicaScreen} />
          <Stack.Screen name="LoginClinica" component={LoginClinicaScreen} />
          <Stack.Screen name="SessaoRestritaClinica" component={SessaoRestritaClinicaScreen} />
          <Stack.Screen name="DadosClinica" component={DadosClinicaScreen} />
          <Stack.Screen name="ConsultarDadosClinica" component={ConsultarDadosClinicaScreen} />
          <Stack.Screen name="CadastrarMedicos" component={CadastrarMedicosScreen} />
          <Stack.Screen name="ConsultarDadosMedicos" component={ConsultarDadosMedicosScreen} />
          <Stack.Screen name="SugestaoServicosClinica" component={SugestaoServicosClinicaScreen} />
        </Stack.Navigator>

      </NavigationContainer>
    </AuthProvider>
  );
}
