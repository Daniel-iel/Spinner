import React from 'react';
import clsx from 'clsx';
import Layout from '@theme/Layout';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import styles from './index.module.css';
import HomepageFeatures from '../components/HomepageFeatures';
import logo from '../../static/assets/spinner-logo200px.png';

import ButtonSpinner from '../components/buttom-spinner/ButtomSpinner';

function HomepageHeader() {
  const { siteConfig } = useDocusaurusContext();
  return (
    <header className={clsx('hero hero--primary', styles.heroBanner)}>
      <div className="container">

        <img src={logo} alt="logo" className={styles.logo} />

        <div className={styles.buttons}>

          <ButtonSpinner
            to={"/docs/intro"}
            text={"Get Started Tutorial - 5min ⏱️"}
          />
    
        </div>
      </div>
    </header>
  );
}

export default function Home() {
  const { siteConfig } = useDocusaurusContext();
  return (
    <Layout
      title={`${siteConfig.title}`}
      description="A simple object mapper to make your code maintainable and readable.">
      <HomepageHeader />
      <main>
        <HomepageFeatures />
      </main>
    </Layout>
  );
}
