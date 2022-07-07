using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public float offset = 0.5f;
    public float animationDuration = 0.125f;
    public int maxHits = -1;

    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!animating && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(Animate());
            }
        }
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + new Vector3(0, offset, 0);

        yield return Animate(restingPosition, animatedPosition);
        yield return Animate(animatedPosition, restingPosition);

        maxHits--;

        if (maxHits == 0) {
            Destroy(this);
        }

        animating = false;
    }

    private IEnumerator Animate(Vector2 from, Vector2 to)
    {
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            float t = elapsed / animationDuration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }

}
