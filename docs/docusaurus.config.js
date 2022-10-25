const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Spinner',
  tagline: 'Spinner Documentation',
  url: 'http://spinnerframework.com/',
  baseUrl: '/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',
  favicon: 'assets/favicon.ico',
  organizationName: 'SpinnerAlloc',
  projectName: 'Spinner',

  presets: [
    [   
      '@docusaurus/preset-classic',      
      ({        
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),      
          editUrl: 'https://github.com/Daniel-iel/Spinner/',
        },     
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
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
            label: 'Documentation',
          },          
          {
            href: 'https://github.com/SpinnerAlloc/Spinner',
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
                href: 'https://github.com/SpinnerAlloc/Spinner/issues',
              },              
            ],
          },
          {
            title: 'More',
            items: [           
              {
                label: 'GitHub',
                href: 'https://github.com/SpinnerAlloc/Spinner',
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
      },
      gtag: {
        trackingID: 'G-QK1XCXPTY5',
        anonymizeIP: true,
      }      
    }),
};

module.exports = config;
