import React from 'react';

export default interface LoanApplicationEvents {
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void,
    onMultiUpdates: (...updates: [string, any][]) => void,
    nextForm: string,
    next: (e: Event) => void,
    onEndOfFlow: () => void
}