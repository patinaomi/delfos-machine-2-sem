import React, { useEffect, useState } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { View, Text, Button, TouchableOpacity, ImageBackground } from 'react-native';
import { StyleSheet } from "react-native";
import { AuthProvider } from "./contexts/AuthProvider";
import { auth } from './src/firebaseConfig';

// Cliente
import CadastroClienteScreen from './screens/CadastroClienteScreen';
import SucessoScreen from './screens/SucessoScreen';
import LoginClienteScreen from './screens/LoginClienteScreen';
import HomeClienteScreen from './screens/HomeClienteScreen';
import SessaoRestritaClienteScreen from './screens/SessaoRestritaClienteScreen';
import CadastrarDadosPessoaisClienteScreen from './screens/CadastrarDadosPessoaisClientesScreen';
import CadastrarEnderecoResidenciaClienteScreen from './screens/CadastrarEnderecoResidenciaClienteScreen';
import CadastrarEnderecoPreferenciaClienteScreen from './screens/CadastrarEnderecoPreferenciaClienteScreen';
import CadastrarDiaPreferenciaClienteScreen from './screens/CadastrarDiaPreferenciaCliente';
import CadastrarTurnoPreferenciaClienteScreen from './screens/CadastrarTurnoClienteScreen';

import DadoPessoalClienteScreen from './screens/DadoPessoalClienteScreen';
import ConsultarDadosClienteScreen from './screens/ConsultarDadosClienteScreen';


import CustomButton from "./components/CustomButton";
import Footer from './components/Footer';

// Explicação sobre o projeto
import OnboardingScreen from './screens/OnboardingScreen';

// Clinica
import HomeClinicaScreen from './screens/HomeClinicaScreen';
import CadastroClinicaScreen from './screens/CadastroClinicaScreen';
import LoginClinicaScreen from './screens/LoginClinicaScreen';
import SessaoRestritaClinicaScreen from './screens/SessaoRestritaClinicaScreen';
import DadosClinicaScreen from './screens/DadosClinicaScreen';
import ConsultarDadosClinicaScreen from './screens/ConsultarDadosClinicaScreen';

// Medicos
import CadastrarMedicosScreen from './screens/CadastrarMedicosScreen';
import ConsultarDadosMedicosScreen from './screens/ConsultarDadosMedicosScreen';

// Sugestão de Serviços
import SugestaoServicosClinicaScreen from './screens/SugestaoServicosClinicaScreen';
import SugestaoServicosClienteScreen from './screens/SugestaoServicosClienteScreen';

// Consultas
import AgendamentosClienteScreen from './screens/AgendamentosClienteScreen';
import AgendamentosClinicaScreen from './screens/AgendamentosClinicaScreen';

// Programa de recompensa
import SobreProgramaBeneficiosScreen from './screens/SobreProgramaBeneficioScreen';
import ProgramaBeneficioClienteScreen from './screens/ProgramaBeneficiosClienteScreen';
import AtividadesProgramaBeneficioClienteScreen from './screens/AtividadesProgramaBeneficioCliente';
import SobreProgramaBeneficiosClinicasScreen from './screens/SobreProgramaBeneficioClinicasScreen';
import ProgramaBeneficioClinicaScreen from './screens/ProgramaBeneficiosClinicaScreen';


// Videos
import VideoScreen from './screens/ConteudoPreventivoClienteScreen'

const Stack = createStackNavigator();

const HomeScreen: React.FC<{ navigation: any }> = ({ navigation }) => {
  return (
    <ImageBackground source={require("../my-app/assets/Home/imagem-um.jpg")} style={styles.background}>
        <View style={styles.container}>
          {/* Título */}
          <Text style={styles.title}>Bem-vindo ao seu Agendamento Inteligente</Text>

            <CustomButton title="para clientes" textColor='#fff' onPress={() => navigation.navigate("HomeCliente")} width={'100%'}/>
  
            <CustomButton title="para parceiros" textColor='#fff' onPress={() => navigation.navigate("HomeClinica")} width={'100%'}/>

        </View>
        {/* Rodapé */}
        <Footer textColor='#fff'/>
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
    color: "#fff",
    textAlign: "left",
    marginBottom: 20,
    marginTop: 200,
    width:'100%'
  },
  titleSecond: {
    fontSize: 22,
    fontWeight: "semibold",
    color: "#049dbf",
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

          {/* Cliente */}
          <Stack.Screen name="Cadastro" component={CadastroClienteScreen} />
          <Stack.Screen name="LoginCliente" component={LoginClienteScreen} />
          <Stack.Screen name="Sucesso" component={SucessoScreen} />
          <Stack.Screen name="HomeCliente" component={HomeClienteScreen} />
          <Stack.Screen name="SessaoRestritaCliente" 
          component={SessaoRestritaClienteScreen} />
          <Stack.Screen name='CadastrarDadosPessoaisClientes' component={CadastrarDadosPessoaisClienteScreen} />
          <Stack.Screen name='CadastrarEnderecoResidenciaCliente' component={CadastrarEnderecoResidenciaClienteScreen} />
          <Stack.Screen name='CadastrarEnderecoPreferenciaCliente' component={CadastrarEnderecoPreferenciaClienteScreen} />
          <Stack.Screen name='CadastrarDiaPreferenciaCliente' component={CadastrarDiaPreferenciaClienteScreen} />
          <Stack.Screen name='CadastrarTurnoPreferenciaCliente' component={CadastrarTurnoPreferenciaClienteScreen} />

          <Stack.Screen name="DadoPessoalCliente" component={DadoPessoalClienteScreen} />
          <Stack.Screen name="ConsultarDadosCliente" component={ConsultarDadosClienteScreen} />
          
          {/* Clinica */}
          <Stack.Screen name="HomeClinica" component={HomeClinicaScreen} />
          <Stack.Screen name="CadastroClinica" component={CadastroClinicaScreen} />
          <Stack.Screen name="LoginClinica" component={LoginClinicaScreen} />
          <Stack.Screen name="SessaoRestritaClinica" component={SessaoRestritaClinicaScreen} />
          <Stack.Screen name="DadosClinica" component={DadosClinicaScreen} />
          <Stack.Screen name="ConsultarDadosClinica" component={ConsultarDadosClinicaScreen} />

          {/* Medicos */}
          <Stack.Screen name="CadastrarMedicos" component={CadastrarMedicosScreen} />
          <Stack.Screen name="ConsultarDadosMedicos" component={ConsultarDadosMedicosScreen} />

          {/* Sugestão de serviços */}
          <Stack.Screen name="SugestaoServicosClinica" component={SugestaoServicosClinicaScreen} />
          <Stack.Screen name="SugestaoServicosCliente" component={SugestaoServicosClienteScreen} />

          {/* Consultas */}
          <Stack.Screen name="AgendamentosCliente" component={AgendamentosClienteScreen} />
          <Stack.Screen name="AgendamentosClinica" component={AgendamentosClinicaScreen} />

          {/* Programa de recompensa */}
          <Stack.Screen name="SobreProgramaBeneficios" component={SobreProgramaBeneficiosScreen} />
          <Stack.Screen name="ProgramaBeneficioCliente" component={ProgramaBeneficioClienteScreen} />
          <Stack.Screen name="AtividadesProgramaBeneficioCliente" component={AtividadesProgramaBeneficioClienteScreen} />

          <Stack.Screen name="SobreProgramaClinicasBeneficios" component={SobreProgramaBeneficiosClinicasScreen} />
          <Stack.Screen name="ProgramaBeneficioClinica" component={ProgramaBeneficioClinicaScreen} />

          {/* Vídeo */}
          <Stack.Screen name="ConteudoPreventivoCliente" component={VideoScreen} />
        </Stack.Navigator>

      </NavigationContainer>
    </AuthProvider>
  );
}
