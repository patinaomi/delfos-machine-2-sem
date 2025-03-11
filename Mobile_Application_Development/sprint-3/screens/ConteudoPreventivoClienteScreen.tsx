// screens/VideoScreen.tsx
import React from 'react';
import { SafeAreaView } from 'react-native';
import VideoList from '../components/VideoList';

const VideoScreen = () => {
  return (
    <SafeAreaView style={{ flex: 1 }}>
      <VideoList />
    </SafeAreaView>
  );
};

export default VideoScreen;
