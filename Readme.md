# Urban Insights: Tanzu Greenplum Showcase

[![Tanzu Greenplum Logo](https://www.vmware.com/content/dam/digitalmarketing/vmware/en/images/logos/tanzu/tanzu-greenplum-logo-color.png)](https://tanzu.vmware.com/greenplum)

Urban Insights is a demonstration application built to showcase the capabilities of Tanzu Greenplum for urban planning, data science, and city management. This project is designed to provide insights into demographics, points of interest, and urban patterns, aiding in data-driven decision-making for city development.

## Key Features

* **Data Ingestion & Integration:** Load and integrate diverse data sources (CSV, APIs) into Greenplum.
* **Geospatial Analysis:** Store, query, and visualize geospatial data using PostGIS. Identify POIs and calculate distances.
* **Machine Learning Integration:** Utilize pgvector for vector similarity searches and run ML models within Greenplum to analyze patterns and predict trends.
* **Advanced Analytics:** Employ complex SQL queries, window functions, CTEs, and visualizations to uncover valuable insights from urban data.

## Demo Scenario

Urban Insights empowers city planners to:

1. **Understand Demographics:** Analyze population data to identify trends and make informed decisions about resource allocation and infrastructure planning.
2. **Find Nearby Amenities:** Locate nearby schools, hospitals, and parks to assess the suitability of locations for new development projects.
3. **Predict Growth:** Use AI/ML to forecast demographic shifts and anticipate future needs, supporting proactive planning and development.
4. **Optimize City Services:** Gain comprehensive insights through reports and visualizations to identify areas for improvement and streamline service delivery.

## Implementation Details

* **Technology Stack:**
    * Tanzu Greenplum: Distributed data warehouse for analytics.
    * PostGIS: Geospatial extension for Greenplum.
    * Python: Machine learning model integration.
    * pgvector: Vector similarity search capabilities.

* **Project Structure:**
    * `Models/`:  Data models for people and locations.
    * `Services/`: Core application logic (database, geospatial, ML).
    * `Data/`: Sample demographic and POI datasets.
    * `Migrations/`: SQL scripts to create tables and load data.

## Getting Started

1. **Prerequisites:**
   * Tanzu Greenplum instance
   * PostGIS extension installed
   * Python environment with required libraries

2. **Installation:**
   * Clone this repository.
   * Configure connection details in `appsettings.json`.
   * Run the migration scripts to create tables.
   * Load the sample data or your own datasets.

## Contributing

We welcome contributions! Please see our [CONTRIBUTING.md](CONTRIBUTING.md) file for guidelines.

## License

This project is licensed under the [MIT License](LICENSE).
