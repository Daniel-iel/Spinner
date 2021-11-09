import React from 'react';
import clsx from 'clsx';
import styles from './ButtomSpinner.module.css';
import Link from '@docusaurus/Link';

export default function ButtonSpinner( { to, text }) {
    return (
        <Link
            className={`button button--lg butomSpinner ${styles.butomSpinner}`}
            to={to}>
                {text}
          </Link>
    );
}
  