import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';

import { observableStore } from '../../core/shared/services/store';

export function authGuard(): Observable<boolean> {
  return observableStore.pipe(
    map((state): boolean => !!state.oidc.user),
    take(1)
  );
}
