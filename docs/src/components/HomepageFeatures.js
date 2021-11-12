import React from 'react';
import clsx from 'clsx';
import styles from './HomepageFeatures.module.css';

const FeatureList = [
  {
    title: 'Easy to Use',
    Svg: require('../../static/assets/simple-usage-150px.svg').default,
    description: (
      <>
        Spinner was designed to be easy to use and intuitive with simple installation by nuget package.
      </>
    ),
  },
  {
    title: 'Focus on What Matters',
    Svg: require('../../static/assets/focus-on-150px.svg').default,
    description: (
      <>
        You don't need to spend too much time maintaining the code, you only need to write the class representation and done.        
      </>
    ),
  },
  {
    title: 'High Cohesion',
    Svg: require('../../static/assets/high-cohesion-150px.svg').default,
    description: (
      <>
        Spinner is designed to be highly cohesive and easy to maintain.        
      </>
    ),
  },
];

function Feature({Svg, title, description}) {
  return (
    <div className={clsx('col col--4')}>
      <div className="text--center">
        <Svg className={styles.featureSvg} alt={title} />
      </div>
      <div className="text--center padding-horiz--md">
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
    </div>
  );
}

export default function HomepageFeatures() {
  return (
    <section className={styles.features}>
      <div className="container">
        <div className="row">
          {FeatureList.map((props, idx) => (
            <Feature key={idx} {...props} />
          ))}
        </div>
      </div>
    </section>
  );
}
