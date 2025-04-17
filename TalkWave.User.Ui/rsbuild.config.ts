import { defineConfig } from '@rsbuild/core';
import { pluginReact } from '@rsbuild/plugin-react';

export default defineConfig({
  plugins: [pluginReact()],
  html: {
    title: 'TalkWave',
    tags: [
      { 
        tag: 'link',
        attrs: {
          href: 'https://fonts.googleapis.com/css2?family=Abel&display=swap',
          rel: 'stylesheet',
        }, 
      },
    ],
  },
  output: {
    assetPrefix: '/TalkWave/',
  },
});
