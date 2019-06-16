using UnityEngine;
using System.Collections;

public class Emotion : MonoBehaviour
{
    public Animator animator;

    private Coroutine b;
    private Transform emoTransform;
    private Vector3 startPosition;

    private void Start()
    {
        emoTransform = animator.transform;
        startPosition = emoTransform.position;
    }

    public void play(string emotion)
    {
        float coef;
        float vy;
        float lifetime;

        lifetime = Random.Range(1f, 5f);
        coef = Random.Range(0.25f, 0.5f) * (Random.Range(-1,1)  < 0 ? -1 : 1);
        vy = Random.Range(0.1f, 0.5f);

        animator.Play("Base Layer." + emotion, 0);

        if (emotion != "Neutral")
        {
            if (b != null)
                StopCoroutine(b);
            b = StartCoroutine(coEmotion(lifetime, vy, coef));
        }
    }

    IEnumerator coEmotion(float time, float vy,float coef)
    {
        float t = 0;
        float vx = 0f;
        while (time > 0)
        {
            vx = 1/(t+1)*Mathf.Cos(t*Mathf.PI) * coef;
            emoTransform.position = emoTransform.position + new Vector3(vx,vy,0) * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            time -= Time.fixedDeltaTime;
            t += Time.fixedDeltaTime;
        }

        emoTransform.position = startPosition;
        animator.Play("Base Layer.Neutral", 0);
    }

}
