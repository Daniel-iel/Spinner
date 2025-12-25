const {themes} = require('prism-react-renderer');
const lightCodeTheme = themes.github;
const darkCodeTheme = themes.dracula;

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Spinner',
  tagline: 'Spinner Documentation',
  url: 'https://daniel-iel.github.io/',
  baseUrl: '/Spinner/',
  onBrokenLinks: 'throw',
  favicon: 'assets/favicon.ico',
  organizationName: 'Daniel-iel',
  projectName: 'Spinner',
  markdown: {
    hooks: {
      onBrokenMarkdownLinks: 'warn',
    },
  },

  presets: [
    [   
      '@docusaurus/preset-classic',      
      ({        
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),      
          editUrl: 'https://github.com/Daniel-iel/Spinner/'                 
        },     
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
        gtag: {
          trackingID: 'G-QK1XCXPTY5',
          anonymizeIP: true,
        }
      }),      
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      navbar: {
        title: '',
        logo: {
          alt: 'Spinner',
          src: 'assets/logo.svg',
        },
        items: [
          {
            type: 'doc',
            docId: 'intro',
            position: 'right',
            label: 'Doc',
          },         
          {
            href: 'https://github.com/Daniel-iel/Spinner',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              {
                label: 'Tutorial',
                to: '/docs/intro',
              },
            ],
          },
          {
            title: 'Community',
            items: [
              {
                label: 'Stack Overflow',
                href: 'https://stackoverflow.com/questions/tagged/spinnerframework',
              },
              {
                label: 'Issues',
                href: 'https://github.com/Daniel-iel/Spinner/issues',
              },              
            ],
          },
          {
            title: 'More',
            items: [           
              {
                label: 'GitHub',
                href: 'https://github.com/Daniel-iel/Spinner',
              },
              {
                label: 'Nuget Package',
                href: 'https://www.nuget.org/packages/Spinner/',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Spinner.`,
      },
      prism: {
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      }     
    }),
};

module.exports = config;
