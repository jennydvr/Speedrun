using UnityEngine;
using System.Collections;

public class MovementTest : MonoBehaviour {

    #region Inspector Variables

    /// <summary>
    /// Valor A de la espiral logaritmica
    /// Controla la apertura de la curva
    /// </summary>
    public float A = 1;

    /// <summary>
    /// Valor B de la espiral logaritmica 
    /// Controla que tan pegadita se desarrolla la curva 
    /// </summary>
    public float B = 1;

    #endregion

    #region Private Variables

	/// <summary>
	/// Mi transform
	/// </summary>
	private Transform Me;

    /// <summary>
    /// Tiempo total
    /// </summary>
    private float TotalTime;

    #endregion

    #region Unity

	/// <summary>
	/// Inicializacion
	/// </summary>
	private void Start() {
        Me = transform;
        TotalTime = Time.time;
	}

	/// <summary>
	/// Actualizacion
	/// </summary>
	private void Update() {
        TotalTime += Time.deltaTime;

        float n = A * Mathf.Exp(B * TotalTime);

        Vector3 newVector = Vector3.zero;
        newVector.x = n * Mathf.Cos(TotalTime);
        newVector.z = n * Mathf.Sin(TotalTime);

        Me.Translate((newVector - Me.position) * Time.deltaTime);

	}

    #endregion

}
